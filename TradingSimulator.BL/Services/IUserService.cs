using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.BL.Models;

namespace TradingSimulator.BL.Services
{
    public interface IUserService
    {
        Task<bool> CheckUniqueUser(string email);
        Task<UserDto> GetUserAsync(string email, string password);
        Task<UserDto> GetUserAsync(int id);
        Task CreateUserAsync(string email, string password);
        Task<UserBalance> GetUserBalanceAsync(int id);
        Task SetBalance(int userId, int sum);
    }
}
