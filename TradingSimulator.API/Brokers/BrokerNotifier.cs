using TradingSimulator.BL.Services;
using TradingSimulator.Web.Models;

namespace TradingSimulator.Web.Services
{
    public class BrokerNotifier : IBrokerNotifier
    {
        private readonly IServiceProvider _provider;
        public BrokerNotifier(IServiceProvider provider)
        {
            _provider = provider;

            using (var scope = _provider.CreateScope())
            {
                var dealService = scope.ServiceProvider.GetRequiredService<IDealService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var deals = dealService.GetDeals();

                foreach (var deal in deals)
                {
                    if ((deal.Status == DAL.Models.DealStatuses.Waiting) ||
                        (deal.Status == DAL.Models.DealStatuses.Open && (deal.StopLoss != 0 || deal.TakeProfit != 0)))
                    {
                        _brokers.Add(new Broker(emailService, deal));
                    }
                }
            }
        }

        private List<IBroker> _brokers = new();

        public void Attach(IBroker broker)
        {
            _brokers.Add(broker);
        }

        public void Detach(IBroker broker)
        {
            _brokers.Remove(broker);
        }

        public async Task Notify(RateInfo rateInfo)
        {
            using (var scope = _provider.CreateScope())
            {
                var dealService = scope.ServiceProvider.GetRequiredService<IDealService>();
                foreach (var broker in _brokers.ToList())
                {
                    if (broker.Closed)
                    {
                        _brokers.Remove(broker);
                        continue;
                    }

                    await broker.Update(rateInfo, dealService);
                }
            }
        }
    }
}