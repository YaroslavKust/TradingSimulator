using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.DAL.Models
{
    public enum ActiveTypes
    {
        Crypto,
        Commodity,
        Index,
        Share
    }
    public class Active
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticket { get; set; }
        public ActiveTypes Type { get; set; }
        public decimal LastBid { get; set; }
        public decimal LastAsk { get; set; }
    }
}
