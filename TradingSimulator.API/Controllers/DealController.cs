using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.BL.Models;
using TradingSimulator.BL.Services;
using TradingSimulator.DAL.Models;
using TradingSimulator.Web.Models;
using TradingSimulator.Web.Services;

namespace TradingSimulator.Web.Controllers
{
    [Route("api/deals")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IMapper _mapper;
        private readonly IBrokerNotifier _notifier;
        private readonly IEmailService _emailService;

        public DealController(
            IDealService dealService, 
            IMapper mapper, 
            IBrokerNotifier notifier, 
            IEmailService emailService)
        {
            _dealService = dealService;
            _mapper = mapper;
            _notifier = notifier;
            _emailService = emailService;
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenDeal([FromBody] DealOpen deal)
        {
            var id = HttpContext.User.FindFirst("id").Value;
            deal.UserId = int.Parse(id);

            var dealDb = _mapper.Map<DealOpen, Deal>(deal);
            await _dealService.CreateDeal(dealDb);

            if (dealDb.Status == DealStatuses.Waiting)
                _notifier.Attach(new Broker(_emailService, dealDb) { ExecuteDealPermanently = deal.ExecutePermanently });
            else if (dealDb.Status == DealStatuses.Open)
            {
                await _dealService.OpenDeal(dealDb);
                if(dealDb.StopLoss != 0 || dealDb.TakeProfit != 0)
                    _notifier.Attach(new Broker(_emailService, dealDb) { ExecuteDealPermanently = deal.ExecutePermanently });
            }
                

            return NoContent();
        }

        [HttpPost("close")]
        public async Task<IActionResult> CloseDeal([FromBody] DealClose deal)
        {
            var dealDb = _mapper.Map<DealClose, Deal>(deal);
            await _dealService.CloseDeal(dealDb);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetDeals()
        {
            var id = HttpContext.User.FindFirst("id").Value;
            var userId = int.Parse(id);
            var deals = _dealService.GetDeals(userId);
            var dealsDto = _mapper.Map<IEnumerable<Deal>, IEnumerable<DealDto>>(deals);
            return Ok(dealsDto);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmDeal([FromQuery] string action, int dealId)
        {
            var deal = _dealService.GetDeals().FirstOrDefault(d => d.Id == dealId);

            if (action == "open")
            {
                await _dealService.OpenDeal(deal);
                return Ok("Deal successfully open!");
            }

            if (action == "close")
            {
                await _dealService.CloseDeal(deal);
                return Ok("Deal successfully closed!");
            }

            return BadRequest("Action type is not recognized");
        }

        [HttpGet("statistic")]
        public IActionResult GetDealsStatistic()
       {
            var id = HttpContext.User.FindFirst("id").Value;
            var userId = int.Parse(id);

            var deals = _dealService.GetDeals(userId).Where(d=>d.Status != DealStatuses.Close)
                .GroupBy(d=>d.Active.Type.ToString())
                .Select(g=> new {Type = g.Key, 
                    Sum = g.Sum(d=>d.Count*(d.Count > 0 ? d.Active.LastBid : d.Active.LastAsk))});

            return Ok(deals);
        }
    }
}
