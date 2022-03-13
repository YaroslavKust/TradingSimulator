using AutoMapper;
using TradingSimulator.DAL;

namespace TradingSimulator.BL.Services
{
    public abstract class BaseDataService
    {
        protected ITradingManager Manager;
        public BaseDataService(ITradingManager manager)
        {
            Manager = manager;
        }
    }
}
