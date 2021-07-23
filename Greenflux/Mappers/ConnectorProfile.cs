using AutoMapper;
using Greenflux.Models.Connectors;
using static Greenflux.Models.ChargeStations.VCreateStationReq;
using static Greenflux.Models.ChargeStations.VCreateStationResp;

namespace Greenflux.Mappers
{
    public class ConnectorProfile : Profile
    {
        public ConnectorProfile()
        {
            CreateMap<VCreateStationConnectorReq, SConnector>();
            CreateMap<SConnector, VCreateStationConnectorResp>();
            CreateMap<SConnector, VConnectorResp>();
            CreateMap<SConnector, VConnectorByStationResp>();
            CreateMap<VCreateConnectorReq, SConnector>();
            CreateMap<VUpdateConnectorReq, SConnector>();
        }
    }
}