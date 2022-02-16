using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class DealRepository: Repository<Deal>, IDealRepository
    {
        public DealRepository(TradingContext context) : base(context) { }

        public void UpdateDeal(Deal deal)
        {
            Context.Deals.Update(deal);
        }
    }
}
