using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Repositories;

namespace TradingSimulator.DAL
{
    public interface ITradingManager
    {
        IUserRepository Users { get;}
        IActiveRepository Actives { get;}
        IOperationRepository Operations { get;}
        IDealRepository Deals { get;}
        Task SaveAsync();
    }
}
