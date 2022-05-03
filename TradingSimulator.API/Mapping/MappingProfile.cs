using AutoMapper;
using TradingSimulator.BL.Models;
using TradingSimulator.DAL.Models;
using TradingSimulator.Web.Models;

namespace TradingSimulator.API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserBalance>();
            CreateMap<DealOpen, Deal>();
            CreateMap<DealClose, Deal>();
            CreateMap<Deal, DealDto>()
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status.ToString()));
        }
    }
}
