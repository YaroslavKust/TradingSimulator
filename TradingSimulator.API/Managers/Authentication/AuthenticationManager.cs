using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradingSimulator.API.Models;
using TradingSimulator.BL.Models;
using TradingSimulator.BL.Services;

namespace TradingSimulator.API.Managers.Authentication
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private IConfiguration _config;
        private IUserService _userService;
        private UserDto _user;


        public AuthenticationManager(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }


        public async Task<bool> ValidateUser(UserAuth user)
        {
            _user = await _userService.GetUserAsync(user.Email, user.Password);
            return _user != null;
        }


        public string GenerateToken()
        {
            var credentials = GetCredentials();
            var claims = GetClaims();

            var token = new JwtSecurityToken(
                _config["JwtSettings:issuer"],
                _config["JwtSettings:audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> CreateUser(UserAuth user)
        {
            var existingUser = await _userService.CheckUniqueUser(user.Email);

            if (existingUser)
            {
                return false;
            }

            await _userService.CreateUserAsync(user.Email, user.Password);
            return true;
        }


        private SigningCredentials GetCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }


        private List<Claim> GetClaims()
        {
            var claims = new List<Claim> {
                new Claim("id", _user.Id.ToString()),
                new Claim(ClaimTypes.Name, _user.Email)
            };
            return claims;
        }
    }
}
