using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class Broker: IBroker
    {
        private Deal _deal;
        private IDealService _dealService;
        public Broker(IDealService dealService)
        {
            _dealService = dealService;
        }

        public void Update(ObserveParameters parameters)
        {
            if((parameters.Price <= _deal.OpenPrice && _deal.Count > 0) || 
                (parameters.Price >= _deal.OpenPrice && _deal.Count < 0))
            {
                _dealService.OpenDeal(_deal);
            }

            if((parameters.Price <= _deal.ClosePrice && _deal.Count < 0) ||
                (parameters.Price >= _deal.ClosePrice && _deal.Count > 0))
            {
                if(_deal.Status == DealStatuses.Open)
                    _dealService.CloseDeal(_deal);
            }
        }
    }
}
