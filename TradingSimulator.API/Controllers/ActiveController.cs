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
    }
}
