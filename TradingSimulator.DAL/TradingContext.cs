using Microsoft.EntityFrameworkCore;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL
{
    public class TradingContext: DbContext
    {
        public TradingContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Active> Actives { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Deal> Deals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ActiveConfiguration());
        }
    }
}
