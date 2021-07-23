using Greenflux.Data;
using Greenflux.Exceptions;
using Greenflux.Models.ChargeStations;
using Greenflux.Models.Connectors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greenflux.Services
{
    public class ChargeStationService
    {
        private readonly GroupService _groupService;
        private readonly ChargeStationRepository _chargeStationRepository;
        private readonly GroupRepository _groupRepository;
        private readonly ConnectorRepository _connectorRepository;

        public ChargeStationService(
            GroupService groupService,
            ChargeStationRepository chargeStationRepository,
            GroupRepository groupRepository,
            ConnectorRepository connectorRepository)
        {
            _groupService = groupService;
            _chargeStationRepository = chargeStationRepository;
            _groupRepository = groupRepository;
            _connectorRepository = connectorRepository;
        }

        public async Task<(SChargeStation chargeStation, IEnumerable<SConnector> connectors)> CreateAsync(SChargeStation chargeStation, IEnumerable<SConnector> connectors)
        {
            bool canCreateStation = await _groupService.CanGroupAddCurrent(chargeStation.GroupId, connectors.Sum(c => c.MaxCurrent));
            if (!canCreateStation)
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE.Throw();

            chargeStation = await _chargeStationRepository.CreateAsync(chargeStation);
            foreach(var connector in connectors)
            {
                connector.ChargeStationId = chargeStation.Id;
                await _connectorRepository.CreateAsync(connector);
            }

            return (chargeStation, connectors);
        }

        public async Task UpdateAsync(SChargeStation station)
        {
            var stationDb = await _chargeStationRepository.FindByIdAsync(station.Id);
            if (stationDb == null)
                AppErrors.CHARGE_STATION_NOT_FOUND.Throw();
            stationDb.Name = station.Name;

            await _chargeStationRepository.UpdateAsync(stationDb);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _connectorRepository.DeleteByChargeStationIdAsync(id);
            await _chargeStationRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteByGroupIdAsync(int groupId)
        {
            await _connectorRepository.DeleteByGroupIdAsync(groupId);
            await _chargeStationRepository.DeleteByGroupIdAsync(groupId);
        }

        public async Task<SChargeStation> GetByIdAsync(int stationId)
        {
            var station = await _chargeStationRepository.FindByIdAsync(stationId);
            if (station == null)
                AppErrors.CHARGE_STATION_NOT_FOUND.Throw();

            return station;
        }

        public Task<IEnumerable<SChargeStation>> FindAllByGroupIdAsync(int groupId) =>
            _chargeStationRepository.FindAllByGroupIdAsync(groupId);
    }
}
