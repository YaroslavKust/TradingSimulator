﻿using AutoMapper;
using TradingSimulator.BL.Models;
using TradingSimulator.DAL;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class UserService: BaseDataService
    {
        private const decimal START_BALANCE = 100000;
        public UserService(ITradingManager manager, IMapper mapper): base(manager, mapper) { }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await Manager.Users.Get(id);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
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
    }
}
