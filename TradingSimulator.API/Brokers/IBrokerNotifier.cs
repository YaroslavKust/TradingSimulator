namespace TradingSimulator.Web.Services
{
    public interface IBrokerNotifier
    {
        void Attach(IBroker observer);
        void Detach(IBroker observer);
        void Notify(decimal price);
    }
}
