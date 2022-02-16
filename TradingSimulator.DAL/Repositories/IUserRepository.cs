using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        void UpdateUser(User user);
    }
}
