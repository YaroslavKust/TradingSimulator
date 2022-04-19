using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using System.Text;
using TradingSimulator.BL.Services;
using TradingSimulator.Web.Services;

namespace TradingSimulator.API.Services
{
    public class RatesHostedService: BackgroundService
    {
        private readonly IHubContext<RateHub> _hub;
        private readonly IBrokerNotifier _brokerNotifier;
        private readonly IServiceProvider _serviceProvider;
        public RatesHostedService(
            IHubContext<RateHub> hub, 
            IBrokerNotifier brokerNotifier, 
            IServiceProvider serviceProvider)
        {
            _hub = hub;
            _brokerNotifier = brokerNotifier;
            _serviceProvider = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var hub = new RateHub();

            var activeService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IActiveService>();

            var actives = activeService.GetActives();

            var tickets = actives.Select(a=>a.Ticket);
            var ticketStringItems = new List<string>();

            foreach (var ticket in tickets)
            {
                ticketStringItems.Add($"\"{ticket}\"");
            }

            var ticketsString = string.Join(',', ticketStringItems);

            using var socket = new ClientWebSocket();

            await socket.ConnectAsync(new Uri("wss://api-adapter.backend.currency.com/connect"), CancellationToken.None);
            string message = "{\"destination\":\"marketData.subscribe\",\"payload\":{\"symbols\":[" + ticketsString + "]}}";

            var bytes = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(bytes);

            await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

            while (!stoppingToken.IsCancellationRequested)
            {
                var bytesRecieved = new ArraySegment<byte>(new byte[1024]);
                await socket.ReceiveAsync(bytesRecieved, CancellationToken.None);
                var response = Encoding.UTF8.GetString(bytesRecieved).Replace("\0", string.Empty);
                await _hub.Clients.All.SendAsync("SendRates", response);
                _brokerNotifier.Notify(120);
            }

        }
    }
}
