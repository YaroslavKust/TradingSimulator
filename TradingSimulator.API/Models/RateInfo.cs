namespace TradingSimulator.Web.Models
{
    //{"status":"OK","destination":"internal.quote","payload":{"symbolName":"BTC/USD","bid":38568.700000000004,"bidQty":4.0,"ofr":38568.8,"ofrQty":4.0,"timestamp":1647176807794}}
    public class RateInfo
    {
        public string SymbolName { get; set; }
        public decimal Bid { get; set; }
        public decimal Ofr { get; set; }
    }
}
