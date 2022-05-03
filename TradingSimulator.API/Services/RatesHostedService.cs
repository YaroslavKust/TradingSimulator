using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Text;
using TradingSimulator.BL.Services;
using TradingSimulator.Web.Models;
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

            var socket = new ClientWebSocket();

            await StartWEbSocketConnection(socket, ticketsString);

            while (!stoppingToken.IsCancellationRequested)
            {    
                try
                {
                    var bytesRecieved = new ArraySegment<byte>(new byte[1024]);
                    await socket.ReceiveAsync(bytesRecieved, stoppingToken);
                    var response = Encoding.UTF8.GetString(bytesRecieved).Replace("\0", string.Empty);

                    var responseObj = JsonConvert.DeserializeObject<JToken>(response);

                    var rateInfo = new RateInfo();

                    if(responseObj["payload"]["symbolName"] != null)
                    {
                        rateInfo.SymbolName = responseObj["payload"]["symbolName"].ToString();
                        rateInfo.Ofr = decimal.Parse(responseObj["payload"]["ofr"].ToString());
                        rateInfo.Bid = decimal.Parse(responseObj["payload"]["bid"].ToString());
                    }

                    await _hub.Clients.All.SendAsync("SendRates", response);
                    await _brokerNotifier.Notify(rateInfo);
                }
                catch(Exception ex)
                {
                     if (socket.State != WebSocketState.Open)
                    {
                        socket = new ClientWebSocket();
                        await StartWEbSocketConnection(socket, ticketsString);
                    }
                }
            }

        }

        private async Task StartWEbSocketConnection(ClientWebSocket socket, string ticketsString)
        {
            await socket.ConnectAsync(new Uri("wss://api-adapter.backend.currency.com/connect"), CancellationToken.None);
            string message = "{\"destination\":\"marketData.subscribe\",\"payload\":{\"symbols\":[" + ticketsString + "]}}";

            var bytes = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(bytes);

            await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
