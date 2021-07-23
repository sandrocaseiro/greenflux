using Greenflux.Data;
using Greenflux.Exceptions;
using Greenflux.Models.Connectors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greenflux.Services
{
    public class ConnectorService
    {
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly ConnectorRepository _connectorRepository;
        private readonly ChargeStationRepository _chargeStationRepository;
        
        public ConnectorService(GroupService groupService, GroupRepository groupRepository, ConnectorRepository connectorRepository, ChargeStationRepository chargeStationRepository)
        {
            _groupService = groupService;
            _groupRepository = groupRepository;
            _connectorRepository = connectorRepository;
            _chargeStationRepository = chargeStationRepository;
        }

        public async Task<SConnector> CreateAsync(SConnector connector)
        {
            var station = await _chargeStationRepository.FindByIdAsync(connector.ChargeStationId);
            if (station == null)
                AppErrors.CHARGE_STATION_NOT_FOUND.Throw();
            
            bool canCreateStation = await _groupService.CanGroupAddCurrent(station.GroupId, connector.MaxCurrent);
            if (!canCreateStation)
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE.Throw();

            var connectorDb = await _connectorRepository.FindByIdAndChargeStationIdAsync(connector.Id, connector.ChargeStationId);
            if (connectorDb != null)
                AppErrors.CONNECTOR_ID_ALREADY_EXISTS.Throw();

            await _connectorRepository.CreateAsync(connector);

            return connector;
        }

        public async Task<SConnector> UpdateAsync(SConnector connector)
        {
            var station = await _chargeStationRepository.FindByIdAsync(connector.ChargeStationId);
            if (station == null)
                AppErrors.CHARGE_STATION_NOT_FOUND.Throw();

            var connectorDb = await _connectorRepository.FindByIdAndChargeStationIdAsync(connector.Id, connector.ChargeStationId);
            if (connectorDb == null)
                AppErrors.CONNECTOR_ID_NOT_FOUND.Throw();

            var group = await _groupRepository.FindByIdAsync(station.GroupId);

            var usedCurrent = await _groupRepository.GetUsedCurrentByIdAsync(station.GroupId);
            if (usedCurrent - connectorDb.MaxCurrent + connector.MaxCurrent > group.Capacity)
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE.Throw();

            await _connectorRepository.UpdateAsync(connector);

            return connector;
        }

        public Task DeleteByIdAsync(int id, int chargeStationId) =>
            _connectorRepository.DeleteByIdAsync(id, chargeStationId);

        public Task DeleteByChargeStationIdAsync(int chargeStationId) =>
            _connectorRepository.DeleteByChargeStationIdAsync(chargeStationId);

        public Task DeleteByGroupIdAsync(int groupId) =>
            _connectorRepository.DeleteByGroupIdAsync(groupId);

        public Task<IEnumerable<SConnector>> FindAllByGroupAsync(int groupId) =>
            _connectorRepository.FindAllByGroupAsync(groupId);

        public Task<IEnumerable<SConnector>> FindAllByChargeStationAsync(int chargeStationId) =>
            _connectorRepository.FindAllByChargeStationAsync(chargeStationId);
    }
}
