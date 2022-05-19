using TradingSimulator.DAL.Models;

namespace TradingSimulator.Web.Models
{
    public class OperationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Sum { get; set; }
    }
}
