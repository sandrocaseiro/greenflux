using System.Collections.Generic;

namespace Greenflux.Models.ChargeStations
{
    public record VCreateStationResp(int Id, string Name, int GroupId)
    {
        public IEnumerable<VCreateStationConnectorResp> Connectors { get; set; }
        public record VCreateStationConnectorResp(int Id, decimal MaxCurrent);
    }
}
