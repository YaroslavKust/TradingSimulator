using TradingSimulator.Web.Models;

namespace TradingSimulator.Web.Services
{
    public interface IBrokerNotifier
    {
        void Attach(IBroker observer);
        void Detach(IBroker observer);
        Task Notify(RateInfo rateInfo);
    }
}
