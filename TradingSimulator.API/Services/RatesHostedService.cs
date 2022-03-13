using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using System.Text;

namespace TradingSimulator.API.Services
{
    public class RatesHostedService: BackgroundService
    {
        private readonly IHubContext<RateHub> _hub;
        public RatesHostedService(IHubContext<RateHub> hub)
        {
            _hub = hub;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var hub = new RateHub();

            using var socket = new ClientWebSocket();

            await socket.ConnectAsync(new Uri("wss://api-adapter.backend.currency.com/connect"), CancellationToken.None);
            string message = "{\"destination\":\"marketData.subscribe\",\"payload\":{\"symbols\":[\"BTC/USD\"]}}";

            var bytes = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(bytes);

            await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);


            while (!stoppingToken.IsCancellationRequested)
            {
                var bytesRecieved = new ArraySegment<byte>(new byte[1024]);
                await socket.ReceiveAsync(bytesRecieved, CancellationToken.None);
                var response = Encoding.UTF8.GetString(bytesRecieved).Replace("\0", string.Empty);
                await _hub.Clients.All.SendAsync("SendRates", response);
            }

        }
    }
}
