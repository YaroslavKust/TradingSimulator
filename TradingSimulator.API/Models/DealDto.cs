using TradingSimulator.DAL.Models;

namespace TradingSimulator.Web.Models
{
    public class DealDto
    {
        public int Id { get; set; }
        public int ActiveId { get; set; }
        public Active Active { get; set; }
        public decimal Count { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public string Status { get; set; }
        public int MarginMultiplier { get; set; }
    }
}
