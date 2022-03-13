using Microsoft.EntityFrameworkCore;
using TradingSimulator.DAL;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class ActiveService: BaseDataService, IActiveService
    {
        public ActiveService(ITradingManager manager): base(manager) { }
        public IEnumerable<Active> GetActives()
        {
            return Manager.Actives.GetAll();
        }

        public async Task<IEnumerable<Active>> GetActives(string[] types)
        {
            var actives = await Manager.Actives.GetAll().ToListAsync();
            return actives.Where(a=>types.Contains(a.Type.ToString()));
        }

        public List<string> GetActiveTypes()
        {
            var types = new List<string>();
            foreach(var type in Enum.GetValues(typeof(ActiveTypes)))
            {
                types.Add(Enum.GetName(typeof(ActiveTypes), type));
            }
            return types;
        }
    }
}
