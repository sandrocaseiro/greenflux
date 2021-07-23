using AutoMapper;
using Greenflux.Models.Groups;

namespace Greenflux.Mappers
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<SGroup, VGroupResp>();
            CreateMap<VCreateGroupReq, SGroup>();
        }
    }
}