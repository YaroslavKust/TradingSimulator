using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Models
{
    public class DealInfo
    {
        public int Id { get; set; }
        public Active Active { get; set; }
        public decimal Count { get; set; }
        public decimal OpenPrice { get; set; }
    }
}
