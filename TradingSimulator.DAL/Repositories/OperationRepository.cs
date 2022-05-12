using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class OperationRepository: Repository<Operation>, IOperationRepository
    {
        public OperationRepository(TradingContext context) : base(context) { }

        public IEnumerable<Operation> GetWithDeals(int userId)
        {
            return Context.Operations.Include(o=>o.Deal).Where(o=>o.Deal.UserId == userId);
        }
    }
}
