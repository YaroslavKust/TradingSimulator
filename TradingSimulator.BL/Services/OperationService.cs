using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class OperationService: BaseDataService, IOperationService
    {
        public OperationService(ITradingManager manager) : base(manager) { }

        public IEnumerable<Operation> GetOperations(int userId)
        {
            return Manager.Operations.GetWithDeals(userId);
        }
    }
}
