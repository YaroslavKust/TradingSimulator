using AutoMapper;
using TradingSimulator.BL.Models;
using TradingSimulator.DAL.Models;

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
        }
    }
}
