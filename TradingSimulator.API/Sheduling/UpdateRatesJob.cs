using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Quartz;
using TradingSimulator.BL.Services;

namespace TradingSimulator.Web.Sheduling
{
    public class UpdateRatesJob: IJob
    {
        private readonly IServiceProvider _serviceProvider;
        public UpdateRatesJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var activeService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IActiveService>();
            var actives = activeService.GetActives();

            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://marketcap.backend.currency.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync($"api/v1/token_crypto/summary");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

            var responseObj = JsonConvert.DeserializeObject<JToken>(result);
            var data = responseObj["data"];

            if(data != null)
            {
                foreach (var active in actives)
                {
                    var ticket = active.Ticket;
                    var activeJson = data[ticket];
                    if (activeJson != null)
                    {
                        active.LastAsk = decimal.Parse(activeJson["lowestAsk"].ToString().Replace('.',','));
                        active.LastBid = decimal.Parse(activeJson["highestBid"].ToString().Replace('.', ','));
                    }
                }
                await activeService.Update(actives);
            }
        }
    }
}
