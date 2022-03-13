using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public interface IActiveService
    {
        IEnumerable<Active> GetActives();
        Task<IEnumerable<Active>> GetActives(string[] types);
        List<string> GetActiveTypes();
    }
}
