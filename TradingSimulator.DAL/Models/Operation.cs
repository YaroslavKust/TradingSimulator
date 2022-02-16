namespace TradingSimulator.DAL.Models
{
    public enum OperationTypes
    {
        Refill,
        OpenDeal,
        CloseDeal,
        OPenDebt,
        CloseDebt,
        OpenDeposit,
        CloseDeposit
    }

    public class Operation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OperationTypes Type { get; set; }
        public decimal Sum { get; set; }
        public int? DealId { get; set; }
        public Deal? Deal { get; set; }
    }
}
