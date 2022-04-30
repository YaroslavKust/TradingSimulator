using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TradingSimulator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetChartData(string symbol)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://marketcap.backend.currency.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync($"api/v1/token_crypto/OHLC?symbol={symbol}");
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
