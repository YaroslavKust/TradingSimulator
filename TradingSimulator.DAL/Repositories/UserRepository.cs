using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(TradingContext context) : base(context) { }

        public void UpdateUser(User User)
        {
            Context.Users.Update(User);
        }
    }
}
