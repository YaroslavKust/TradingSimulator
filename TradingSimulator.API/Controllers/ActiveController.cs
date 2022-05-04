using Microsoft.AspNetCore.Mvc;
using TradingSimulator.BL.Services;
using TradingSimulator.DAL.Models;

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

        [HttpGet("types")]
        public IActionResult GetActiveTypes()
        {
            var types = _activeService.GetActiveTypes();
            return Ok(types);
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetActiveStatistic(string symbol)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api-adapter.backend.currency.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync($"api/v1/ticker/24hr?symbol={symbol}");
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
