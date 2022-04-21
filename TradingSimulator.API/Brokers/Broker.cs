using TradingSimulator.DAL.Models;
using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public class Broker: IBroker
    {
        private Deal _deal;
        public Broker(Deal deal)
        {
            _deal = deal;
        }

        public async Task<bool> Update(ObserveParameters parameters, IDealService dealService)
        {
            if (Around(parameters.Price, _deal.OpenPrice))
            {
                await dealService.OpenDeal(_deal);
                Closed = true;           
                return true;
            }

            if (Around(parameters.Price, _deal.ClosePrice))
            {
                if (_deal.Status == DealStatuses.Open)
                {
                    await dealService.CloseDeal(_deal);
                    Closed = true;
                    return true;
                }                   
            }
            Closed = false;
            return false;
        }

        private bool Around(decimal price, decimal target)
        {
            decimal delta = 0.5M;
            return (price <= target + delta && price >= target - delta);
        }

        public bool Closed { get; set; } = false;
    }
}
