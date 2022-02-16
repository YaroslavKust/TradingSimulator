using AutoMapper;
using TradingSimulator.DAL;

namespace TradingSimulator.BL.Services
{
    public abstract class BaseDataService
    {
        protected ITradingManager Manager;
        protected IMapper Mapper;
        public BaseDataService(ITradingManager manager, IMapper mapper)
        {
            Manager = manager;
            Mapper = mapper;
        }
    }
}
