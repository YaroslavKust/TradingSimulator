using TradingSimulator.BL.Models;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public interface IDealService
    {
        Task CreateDeal(Deal deal);
        Task OpenDeal(Deal deal);
        Task CloseDeal(Deal deal);
        IEnumerable<Deal> GetDeals(int userId);
        IEnumerable<Deal> GetDeals();
    }
}
