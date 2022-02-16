
using System.Linq.Expressions;

namespace TradingSimulator.DAL.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected TradingContext Context;

        public Repository(TradingContext context)
        {
            Context = context;
        }
        public IQueryable<T> GetAll()
        {
           return Context.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }

        public async Task Create(T Entity)
        {
            await Context.Set<T>().AddAsync(Entity);
        }
    }
}
