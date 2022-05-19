using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TradingSimulator.BL.Services;
using TradingSimulator.DAL.Models;
using TradingSimulator.Web.Models;

namespace TradingSimulator.API.Controllers
{
    [Route("api/actives")]
    [ApiController]
    public class ActiveController : ControllerBase
    {
        private IActiveService _activeService;

        public ActiveController(IActiveService activeService)
        {
            _activeService = activeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActives([FromQuery] string[] types)
        {
            IEnumerable<Active> actives;
            if (types.Length != 0)
            {
                actives = await _activeService.GetActives(types);
            }
            else
            {
                actives = _activeService.GetActives();
            }
            return Ok(actives);
        }

        [HttpGet("statistic/{symbol}")]
        public async Task<IActionResult> GetActiveStatistic(string symbol)
        {
            string result = "";
            var correctSymbol = symbol;
            if (correctSymbol.Contains("%2F"))
            {
                correctSymbol = correctSymbol.Replace("%2F", "/");
            }
            var activeName = _activeService.GetActives().FirstOrDefault(a => a.Ticket == correctSymbol)?.Name;
            if(activeName == null)
            {
                return NotFound();
            }
                
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api-adapter.backend.currency.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync($"api/v1/ticker/24hr?symbol={symbol}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    var resultJson = JsonConvert.DeserializeObject<JToken>(result);
                    var activeDto = new ActiveDto()
                    {
                        Name = activeName,
                        Ticket = correctSymbol,
                        Price = decimal.Parse(resultJson["lastPrice"].ToString()),
                        PercentChange = decimal.Parse(resultJson["priceChangePercent"].ToString())
                    };
                    return Ok(activeDto);

                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("historical/{symbol}/{interval}")]
        public async Task<IActionResult> GetChartData(string symbol, string interval)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://marketcap.backend.currency.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.
                    GetAsync($"api/v1/token_crypto/OHLC?symbol={symbol}&interval={interval}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            return Ok(result);
        }
    }
}
