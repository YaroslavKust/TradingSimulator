using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Services
{
    public interface IBroker
    {
        Task<bool> Update(ObserveParameters parameters, IDealService dealService);
        bool Closed { get; set; }
    }
}
