using Microsoft.EntityFrameworkCore;
using TradingSimulator.DAL;


namespace TradingSimulator.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            return services.AddDbContext<TradingContext>(options => options.UseSqlServer(connection));
        }
    }
}
