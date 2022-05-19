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

            CreateMap<Operation, OperationDto>()
                .ForMember(dest => dest.Type, act => act.MapFrom(src => MapOperationType(src.Type)));
        }

        private string MapOperationType(OperationTypes type)
        {
            switch (type)
            {
                case OperationTypes.OpenDeal:
                    return "Открытие сделки";
                case OperationTypes.CloseDeal:
                    return "Закрытие сделки";
                case OperationTypes.OpenDebt:
                    return "Открытие кредита";
                case OperationTypes.CloseDebt:
                    return "Закрытие кредита";
                case OperationTypes.OpenDeposit:
                    return "Открытие залога";
                case OperationTypes.CloseDeposit:
                    return "Закрытие залога";
                default:
                    return "Другое";
            }
        }
    }
}
