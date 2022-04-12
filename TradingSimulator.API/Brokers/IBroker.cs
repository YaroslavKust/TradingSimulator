using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.Web.Services
{
    public interface IBroker
    {
        bool Update(ObserveParameters parameters);
        bool Closed { get; set; }
    }
}
