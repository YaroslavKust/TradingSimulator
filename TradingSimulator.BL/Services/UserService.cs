using Microsoft.EntityFrameworkCore;
using TradingSimulator.BL.Models;
using TradingSimulator.DAL;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class UserService: BaseDataService, IUserService
    {
        private const decimal START_BALANCE = 100000;
        public UserService(ITradingManager manager): base(manager) { }

        public async Task<bool> CheckUniqueUser(string email)
        {
            var user = await Manager.Users.GetByExpression(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await Manager.Users.Get(id);
            var userDto = new UserDto
            {
                Email = user.Email,
                Id = user.Id
            };
            return userDto;
        }

        public async Task<UserDto> GetUserAsync(string email, string password)
        {
            var user = 
                await Manager.Users.GetByExpression(u=>u.Email == email && u.Password == password).FirstOrDefaultAsync();
            if (user != null)
            {
                var userDto = new UserDto
                {
                    Email = user.Email,
                    Id = user.Id
                };
                return userDto;
            }
            else
                return null;
            
        }

        public async Task CreateUserAsync(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password,
                Balance = START_BALANCE
            };
            await Manager.Users.Create(user);
            await Manager.SaveAsync();
        }

        public async Task<UserBalance> GetUserBalanceAsync(int id)
        {
            var user = await Manager.Users.Get(id);
            var balance = new UserBalance 
            { 
                Balance = user.Balance, 
                Debt = user.Debt, 
                Deposit = user.Deposit 
            };
            return balance;
        }

        public async Task SetBalance(int userId, int sum)
        {
            var user = await Manager.Users.Get(userId);
            user.Balance = sum;
            Manager.Users.UpdateUser(user);
            await Manager.SaveAsync();
        }

        public TradingStatistic GetIncomes(string period, int userId)
        {
            DateTime beginDate, endDate = DateTime.Now;

            switch (period)
            {
                case "day":
                    beginDate = DateTime.Now.Date;
                    break;
                case "week":
                    beginDate = DateTime.Now.AddDays(-7);
                    break;
                default:
                    return null;
            }

            var deals = Manager.Deals.GetByExpression(d=>d.UserId == userId).ToList().Where(d => 
            d.UserId == userId && 
            d.Status == DealStatuses.Close &&
            d.CloseTime >= beginDate && 
            d.CloseTime <= endDate);

            var incomes = deals.Select(d => CalculateIncome(d)).Sum();
            var totalSpend = deals.Select(d => d.OpenPrice * Math.Abs(d.Count)).Sum();
            var incomePercents = incomes / totalSpend * 100;

            var sucessesAndFails = deals
                .GroupBy(d => CalculateIncome(d) > 0)
                .Select(g=> new {Key = g.Key, Value = g.Count()});

            var successes = sucessesAndFails.FirstOrDefault(x => x.Key == true)?.Value;
            var fails = sucessesAndFails.FirstOrDefault(x => x.Key == false)?.Value;

            var statistic = new TradingStatistic()
            {
                DealsCount = deals.Count(),
                DealsSuccessed = successes ??= 0,
                DealsFailed = fails ??= 0,
                Income = incomes,
                IncomePercents = incomePercents,
            };

            return statistic;
        }

        private decimal CalculateIncome(Deal deal)
        {
            var difference = (deal.ClosePrice - deal.OpenPrice) * deal.Count;
            return difference;
        }
    }
}
