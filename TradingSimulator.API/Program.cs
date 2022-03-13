using TradingSimulator.API;
using TradingSimulator.API.Extensions;
using TradingSimulator.API.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDataConnection(configuration);
services.ConfigureMapping();
services.ConfigureAuthentication(configuration);
services.AddAuthorization();
services.AddScopedServices();
services.AddHostedService<RatesHostedService>();
services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapHub<RateHub>("/rates"));
app.MapControllers();

app.Run();
