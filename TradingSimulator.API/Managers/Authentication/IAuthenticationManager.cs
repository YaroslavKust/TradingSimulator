using TradingSimulator.API.Models;

namespace TradingSimulator.API.Managers.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuth user);
        string GenerateToken();
        Task<bool> CreateUser(UserAuth user);
    }
}
