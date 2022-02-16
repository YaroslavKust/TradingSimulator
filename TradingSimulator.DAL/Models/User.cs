using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Debt { get; set; }
    }
}
