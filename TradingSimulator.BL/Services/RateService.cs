namespace TradingSimulator.BL.Services
{
    public class RateService
    {
        public IEnumerable<decimal> GetRates(string ticket, int period)
        {
            //get rates
            return new List<decimal>();
        }

        public decimal GetLastRate(string ticket)
        {
            //get rate
            return 1;
        }
    }
}
