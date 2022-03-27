﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.BL.Models;
using TradingSimulator.BL.Services;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.Web.Controllers
{
    [Route("api/deals")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IMapper _mapper;

        public DealController(IDealService dealService, IMapper mapper)
        {
            _dealService = dealService;
            _mapper = mapper;
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenDeal([FromBody] DealOpen deal)
        {
            var id = HttpContext.User.FindFirst("id").Value;
            deal.UserId = int.Parse(id);
            var dealDb = _mapper.Map<DealOpen, Deal>(deal);
            await _dealService.CreateDeal(dealDb);
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
            return Ok(deals);
        }
    }
}
