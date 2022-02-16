using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class ActiveRepository: Repository<Active>, IActiveRepository
    {
        public ActiveRepository(TradingContext context) : base(context) { }
    }
}
