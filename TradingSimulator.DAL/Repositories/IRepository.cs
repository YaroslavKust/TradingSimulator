
using System.Linq.Expressions;

namespace TradingSimulator.DAL.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByExpression(Expression<Func<T, bool>> expression);
        Task<T> Get(int id); 
        Task Create(T entity);
    }
}
