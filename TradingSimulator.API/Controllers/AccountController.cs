using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.API.Managers.Authentication;
using TradingSimulator.API.Models;
using TradingSimulator.BL.Services;

namespace TradingSimulator.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAuthenticationManager _authManager;
        private IUserService _userService;

        public AccountController(IAuthenticationManager authManager, IUserService userService)
        {
            _authManager = authManager;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserAuth user)
        {
            if (await _authManager.ValidateUser(user))
            {
                return Ok(new { Token = _authManager.GenerateToken() });
            }

            return Unauthorized();
        }


        [HttpPost("registration")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserAuth user)
        {
            if (await _authManager.CreateUser(user))
            {
                return StatusCode(201);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id").Value;
            if(userId is null)
                return BadRequest();
            var balance = await _userService.GetUserBalanceAsync(Int32.Parse(userId));
            if(balance == null)
                return NotFound();
            return Ok(balance);
        }
    }
}
