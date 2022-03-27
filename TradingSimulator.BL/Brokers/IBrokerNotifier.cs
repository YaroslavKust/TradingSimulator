using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public interface IBrokerNotifier
    {
        void Attach(IBroker observer);
        void Detach(IBroker observer);
        void Notify();
    }
}
