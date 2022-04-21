using TradingSimulator.DAL;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.BL.Services
{
    public class DealService: BaseDataService, IDealService
    {
        public DealService(ITradingManager manager): base(manager) { }

        public async Task CreateDeal(Deal deal)
        {
            await Manager.Deals.Create(deal);
            await Manager.SaveAsync();
        }

        public async Task OpenDeal(Deal deal)
        {
            var user = await Manager.Users.Get(deal.UserId);

            if (deal.MarginMultiplier > 1)
            {
                await OpenDebt(deal, user);
            }

            if (deal.Count < 0)
            {
                await OpenDeposit(deal, user);
            }

            var operation = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.OpenDeal,
                Sum = deal.Count * deal.OpenPrice,
                DealId = deal.Id
            };
            await Manager.Operations.Create(operation);
                 
            user.Balance -= operation.Sum;

            await Manager.SaveAsync();
        }

        public async Task CloseDeal(Deal deal)
        {
            var dealDb = await Manager.Deals.Get(deal.Id);
            dealDb.ClosePrice = deal.ClosePrice;
            dealDb.Status = DealStatuses.Close;

            var user = await Manager.Users.Get(deal.UserId);

            var operation = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.CloseDeal,
                Sum = deal.Count * deal.ClosePrice,
                DealId = deal.Id
            };
            await Manager.Operations.Create(operation);
            user.Balance += operation.Sum;

            if (deal.Count < 0)
            {
                await CloseDeposit(deal, user);
            }

            if (deal.MarginMultiplier > 1)
            {
                await CloseDebt(deal, user);
            }

            await Manager.SaveAsync();
        }

        public IEnumerable<Deal> GetDeals(int userId)
        {
            var deals = Manager.Deals.GetByExpression(d=>d.UserId ==  userId);
            return deals;
        }



        private async Task CloseDebt(Deal deal, User user)
        {
            var operationDebt = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.CloseDebt,
                Sum = (deal.Count * deal.OpenPrice) * (1 - 1 / deal.MarginMultiplier)
            };

            await Manager.Operations.Create(operationDebt);
            var debt = operationDebt.Sum;
            user.Debt -= debt;
            user.Balance -= debt;
        }

        private async Task OpenDebt(Deal deal, User user)
        {
            var operationDebt = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.OPenDebt,
                Sum = (deal.Count * deal.OpenPrice) * (1 - 1 / deal.MarginMultiplier),
                DealId = deal.Id
            };

            await Manager.Operations.Create(operationDebt);
            var debt = operationDebt.Sum;
            user.Debt += debt;
            user.Balance += debt;
        }

        private async Task OpenDeposit(Deal deal, User user)
        {
            var operationDeposit = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.OpenDeposit,
                Sum = -deal.Count * deal.OpenPrice,
                DealId = deal.Id
            };

            await Manager.Operations.Create(operationDeposit);

            var deposit = operationDeposit.Sum;
            user.Deposit += deposit;
            user.Balance -= deposit;
        }

        private async Task CloseDeposit(Deal deal, User user)
        {
            var operationDeposit = new Operation
            {
                Date = DateTime.Now,
                Type = OperationTypes.CloseDeposit,
                Sum = -deal.Count * deal.OpenPrice,
                DealId = deal.Id
            };

            await Manager.Operations.Create(operationDeposit);
            var deposit = operationDeposit.Sum;
            user.Deposit -= deposit;
            user.Balance += deposit;
        }
    }
}
