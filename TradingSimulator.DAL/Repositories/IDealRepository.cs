using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public interface IDealRepository: IRepository<Deal>
    {
        void UpdateDeal(Deal deal);
    }
}
