using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.BL.Models
{
    public class UserBalance
    {
        public decimal Balance { get; set; }
        public decimal Debt { get; set; }
        public decimal Deposit { get; set; }
    }
}
