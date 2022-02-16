using TradingSimulator.DAL.Repositories;

namespace TradingSimulator.DAL
{
    public class TradingManager: ITradingManager
    {
        private IUserRepository _users;
        private IActiveRepository _actives;
        private IOperationRepository _operations;
        private IDealRepository _deals;
        private TradingContext _context;

        public TradingManager(TradingContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IActiveRepository Actives => _actives ??= new ActiveRepository(_context);
        public IOperationRepository Operations => _operations ??= new OperationRepository(_context);
        public IDealRepository Deals => _deals ??= new DealRepository(_context);

        public Task SaveAsync()=> _context.SaveChangesAsync();
    }
}
