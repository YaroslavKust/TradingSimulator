using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Models
{
    public class DealOpen
    {
        public int ActiveId { get; set; }
        public int UserId { get; set; }
        public decimal Count { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public DealStatuses Status { get; set; }
        public int MarginMultiplier { get; set; }
    }
}
}
