using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public interface IActiveRepository: IRepository<Active>
    {
        void UpdateActives(IEnumerable<Active> actives);
    }
}
