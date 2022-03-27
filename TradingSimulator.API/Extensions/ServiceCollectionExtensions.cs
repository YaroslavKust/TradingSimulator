using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TradingSimulator.API.Managers.Authentication;
using TradingSimulator.API.Mapping;
using TradingSimulator.BL.Services;
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

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = jwtSettings["key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("issuer").Value,
                    ValidAudience = jwtSettings.GetSection("audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<ITradingManager, TradingManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActiveService, ActiveService>();
            services.AddScoped<IDealService, DealService>();
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
