using AutoMapper;
using TradingSimulator.BL.Models;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserBalance>();
        }
    }
}
