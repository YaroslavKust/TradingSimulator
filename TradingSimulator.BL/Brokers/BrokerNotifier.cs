using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public class BrokerNotifier: IBrokerNotifier
    {
        private List<IBroker> _brokers = new();
        public decimal CurrentPrice { get; set; }

        public void Attach(IBroker broker)
        {
            _brokers.Add(broker);
        }

        public void Detach(IBroker broker)
        {
            _brokers.Remove(broker);
        }

        public void Notify()
        {
            foreach(var broker in _brokers)
            {
                var parameters = new ObserveParameters { Price = CurrentPrice };
                broker.Update(parameters);
            }
        }
    }
}
