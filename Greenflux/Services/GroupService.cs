using Greenflux.Data;
using Greenflux.Exceptions;
using Greenflux.Models.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greenflux.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly ChargeStationRepository _chargeStationRepository;
        private readonly ConnectorRepository _connectorRepository;

        public GroupService(
            GroupRepository groupRepository,
            ChargeStationRepository chargeStationRepository,
            ConnectorRepository connectorRepository)
        {
            _groupRepository = groupRepository;
            _chargeStationRepository = chargeStationRepository;
            _connectorRepository = connectorRepository;
        }

        public Task<SGroup> CreateAsync(SGroup group) => _groupRepository.CreateAsync(group);

        public async Task UpdateAsync(SGroup group)
        {
            var groupDb = await _groupRepository.FindByIdAsync(group.Id);
            if (groupDb == null)
                AppErrors.GROUP_NOT_FOUND.Throw();

            var usedCurrent = await _groupRepository.GetUsedCurrentByIdAsync(group.Id);
            if (usedCurrent > group.Capacity)
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE.Throw();

            groupDb.Name = group.Name;
            groupDb.Capacity = group.Capacity;

            await _groupRepository.UpdateAsync(groupDb);
        }

        public async Task UpdateNameAsync(int id, string name)
        {
            var group = await _groupRepository.FindByIdAsync(id);
            if (group == null)
                AppErrors.GROUP_NOT_FOUND.Throw();
            group.Name = name;

            await _groupRepository.UpdateAsync(group);
        }

        public async Task UpdateCapacityAsync(int id, decimal capacity)
        {
            var group = await _groupRepository.FindByIdAsync(id);
            if (group == null)
                AppErrors.GROUP_NOT_FOUND.Throw();
            
            var usedCurrent = await _groupRepository.GetUsedCurrentByIdAsync(id);
            if (usedCurrent > group.Capacity)
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE.Throw();

            group.Capacity = capacity;

            await _groupRepository.UpdateAsync(group);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var group = await _groupRepository.FindByIdAsync(id);
            if (group == null)
                AppErrors.GROUP_NOT_FOUND.Throw();

            await _connectorRepository.DeleteByGroupIdAsync(id);
            await _chargeStationRepository.DeleteByGroupIdAsync(id);
            await _groupRepository.DeleteByIdAsync(id);
        }

        public async Task<SGroup> GetByIdAsync(int groupId)
        {
            var group = await _groupRepository.FindByIdAsync(groupId);
            if (group == null)
                AppErrors.GROUP_NOT_FOUND.Throw();

            return group;
        }

        public Task<SGroup> FindByIdAsync(int groupId) => _groupRepository.FindByIdAsync(groupId);

        public async Task<bool> CanGroupAddCurrent(int groupId, decimal current)
        {
            var group = await _groupRepository.FindByIdAsync(groupId);
            if (group == null)
                AppErrors.GROUP_NOT_FOUND.Throw();

            var used = await _groupRepository.GetUsedCurrentByIdAsync(groupId);
            return used + current <= group.Capacity;
        }

        public Task<IEnumerable<SGroup>> FindAllAsync() => _groupRepository.FindAllAsync();
    }
}