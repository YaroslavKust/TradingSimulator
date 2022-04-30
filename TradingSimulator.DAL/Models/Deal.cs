
namespace TradingSimulator.DAL.Models
{
    public enum DealStatuses
    {
        Open,
        Close,
        Waiting
    }

    public class Deal
    {
        public int Id { get; set; }
        public int ActiveId { get; set; }
        public Active Active { get; set; }
        public decimal Count { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DealStatuses Status { get; set; }
        public int MarginMultiplier { get; set; }
    }
}
