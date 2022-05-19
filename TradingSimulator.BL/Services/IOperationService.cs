using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public interface IOperationService
    {
        IEnumerable<Operation> GetOperations(int userId);
    }
}
