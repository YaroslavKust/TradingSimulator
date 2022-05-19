using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.BL.Services;
using TradingSimulator.Web.Models;

namespace TradingSimulator.Web.Controllers
{
    [Route("api/operations")]
    [ApiController]
    [Authorize]
    public class OperationController : ControllerBase
    {
        private IOperationService _operationService;
        private IMapper _mapper;

        public OperationController(IOperationService operationService, IMapper mapper)
        {
            _operationService = operationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOperations()
        {
            var id = HttpContext.User.FindFirst("id").Value;
            var userId = int.Parse(id);
            var operations = _operationService.GetOperations(userId);
            var operationsDto = _mapper.Map<IEnumerable<OperationDto>>(operations);
            return Ok(operationsDto);
        }
    }
}
