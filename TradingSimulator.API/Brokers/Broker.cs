using TradingSimulator.DAL.Models;
using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public class Broker: IBroker
    {
        private Deal _deal;
        private IDealService _dealService;
        public Broker(IDealService dealService, Deal deal)
        {
            _dealService = dealService;
            _deal = deal;
        }

        public bool Update(ObserveParameters parameters)
        {
            if (Around(parameters.Price, _deal.OpenPrice))
            {
                _dealService.OpenDeal(_deal);
                Closed = true;
                return true;
            }

            if (Around(parameters.Price, _deal.ClosePrice))
            {
                if (_deal.Status == DealStatuses.Open)
                {
                    _dealService.CloseDeal(_deal);
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
