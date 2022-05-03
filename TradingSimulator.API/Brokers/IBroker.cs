using TradingSimulator.BL.Services;
using TradingSimulator.Web.Models;

namespace TradingSimulator.Web.Services
{
    public interface IBroker
    {
        Task<bool> Update(RateInfo rateInfo, IDealService dealService);
        bool Closed { get; set; }
    }
}
