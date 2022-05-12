using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.BL.Models
{
    public class TradingStatistic
    {
        public int DealsCount {get;set;}
        public int DealsSuccessed { get; set; }
        public int DealsFailed { get; set; }
        public decimal Income { get; set; }
        public decimal IncomePercents { get; set; }
    }
}
