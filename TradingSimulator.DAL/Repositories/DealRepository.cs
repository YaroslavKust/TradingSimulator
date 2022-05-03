using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class DealRepository: Repository<Deal>, IDealRepository
    {
        public DealRepository(TradingContext context) : base(context) { }

        public void UpdateDeal(Deal deal)
        {
            Context.Deals.Update(deal);
        }

        public override IQueryable<Deal> GetAll()
        {
            return Context.Deals.Include(d=>d.Active);
        }

        public override async Task<Deal> Get(int id)
        {
            return await Context.Deals.Include(d=>d.Active).FirstOrDefaultAsync(d=>d.Id == id);
        }

        public override IQueryable<Deal> GetByExpression(Expression<Func<Deal, bool>> expression)
        {
            return Context.Deals.Include(d => d.Active).Where(expression);
        } 
    }
}
