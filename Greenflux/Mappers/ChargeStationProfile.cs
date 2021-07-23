using AutoMapper;
using Greenflux.Models.ChargeStations;

namespace Greenflux.Mappers
{
    public class ChargeStationProfile : Profile
    {
        public ChargeStationProfile()
        {
            CreateMap<VCreateStationReq, SChargeStation>();
            CreateMap<SChargeStation, VCreateStationResp>();
            CreateMap<SChargeStation, VChargeStationResp>();
            CreateMap<VUpdateChargeStationReq, SChargeStation>();
            CreateMap<SChargeStation, VChargeStationByIdResp>();
        }
    }
}