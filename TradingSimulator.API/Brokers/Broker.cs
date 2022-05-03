using TradingSimulator.DAL.Models;
using TradingSimulator.BL.Services;
using TradingSimulator.Web.Models;

namespace TradingSimulator.Web.Services
{
    public class Broker : IBroker
    {
        private Deal _deal;
        private IEmailService _emailService;

        public bool ExecuteDealPermanently { get; set; }
        public Broker(IEmailService emailService, Deal deal)
        {
            _emailService = emailService;
            _deal = deal;
        }

        public async Task<bool> Update(RateInfo rateInfo, IDealService dealService)
        {
            if(_deal.Active.Ticket != rateInfo.SymbolName)
            {
                return false;
            }

            if (Around(rateInfo.Bid, _deal.OpenPrice) && _deal.Status == DealStatuses.Waiting)
            {
                if(_deal.Count > 0)
                {
                    _deal.OpenPrice = rateInfo.Bid;
                }
                else
                {
                    _deal.OpenPrice = rateInfo.Ofr;
                }

                if (ExecuteDealPermanently)
                {
                    await dealService.OpenDeal(_deal);
                    Closed = true;
                    _emailService.SendNotification("a", "b", "Deal was opened");
                    return true;
                }
                else
                {
                    _emailService.SendConfirmation("a", "b", "You can open deal",
                        $"https://localhost:7028/api/deals/confirm?action=open&dealId={_deal.Id}");
                    Closed = true;
                    return true;
                }

            }

            var stopLossDerived = _deal.Count > 0 ? Around(rateInfo.Ofr, _deal.StopLoss) : Around(rateInfo.Bid, _deal.StopLoss);
            var takeProfitDerived = _deal.Count > 0 ? Around(rateInfo.Ofr, _deal.TakeProfit) : Around(rateInfo.Bid, _deal.TakeProfit);

            if (stopLossDerived || takeProfitDerived)
            {
                if (_deal.Status == DealStatuses.Open)
                {
                    if (ExecuteDealPermanently)
                    {
                        _deal.ClosePrice = stopLossDerived ? _deal.StopLoss : _deal.TakeProfit;
                        await dealService.CloseDeal(_deal);
                        Closed = true;
                        _emailService.SendNotification("a", "b", "Deal was closed");
                        return true;
                    }
                    else
                    {
                        _emailService.SendConfirmation("a", "b", "You can close deal",
                        $"https://localhost:7028/api/deals/confirm?action=close&dealId={_deal.Id}");
                        Closed = true;
                        return true;
                    }
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