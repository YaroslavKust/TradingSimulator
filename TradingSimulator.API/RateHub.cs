using Microsoft.AspNetCore.SignalR;

namespace TradingSimulator.API
{
    public class RateHub: Hub
    {
        public async Task Send(string message)
        {
            if(Clients != null)
                await Clients.All.SendAsync("SendRates", message);
            else
                Console.WriteLine(message);
        }
    }
}
