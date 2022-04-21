using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public class BrokerNotifier: IBrokerNotifier
    {
        private readonly IServiceProvider _provider;
        public BrokerNotifier(IServiceProvider provider)
        {
            _provider = provider;
            using(var scope = _provider.CreateScope())
            {
                var dealService = scope.ServiceProvider.GetRequiredService<IDealService>();
                var deals = dealService.GetDeals(1);

                foreach (var deal in deals)
                {
                    if (deal.Status == DAL.Models.DealStatuses.Close)
                        deal.Status = DAL.Models.DealStatuses.Open;
                    _brokers.Add(new Broker(deal));
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

        public async Task Notify(decimal price)
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
                    
                    var parameters = new ObserveParameters { Price = price };                  
                    var result = await broker.Update(parameters, dealService);
                }
            }
        }
    }
}
