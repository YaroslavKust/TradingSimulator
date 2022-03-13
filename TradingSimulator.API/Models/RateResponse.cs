namespace TradingSimulator.Web.Models
{
    //{"status":"OK","destination":"internal.quote","payload":{"symbolName":"BTC/USD","bid":38568.700000000004,"bidQty":4.0,"ofr":38568.8,"ofrQty":4.0,"timestamp":1647176807794}}
    public class RateResponse
    {
        public string Status { get; set; }
        public string Destination { get; set; }
        public Payload Payload { get; set; }
    }

    public class Payload
    {
        public string SymbolName { get; set; }
        public double Bid { get; set; }
        public double BidQty { get; set; }
        public double Ofr { get; set; }
        public double OfrQty { get; set; }
        public long Timestamp { get; set; }
    }
}
