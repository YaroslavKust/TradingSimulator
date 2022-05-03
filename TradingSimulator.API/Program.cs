using Quartz;
using TradingSimulator.API;
using TradingSimulator.API.Extensions;
using TradingSimulator.API.Services;
using TradingSimulator.Web.Services;
using TradingSimulator.Web.Sheduling;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var updateRatesJobKey = new JobKey("UpdateRatesJob");
    q.AddJob<UpdateRatesJob>(opts => opts.WithIdentity(updateRatesJobKey));
    q.AddTrigger(opts => opts
        .ForJob(updateRatesJobKey)
        .WithIdentity("UpdateRatesJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(5)
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

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
services.AddSingleton<IBrokerNotifier, BrokerNotifier>();
services.AddCors();

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
app.UseCors(builder=>builder.AllowAnyHeader().WithOrigins("https://localhost:3000").AllowCredentials().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapHub<RateHub>("/rates"));
app.MapControllers();
 
app.Run();
