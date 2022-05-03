using Quartz.Impl;
using Quartz;

namespace TradingSimulator.Web.Sheduling
{
    public class SchedulerTask
    {
        private static readonly string ScheduleCronExpression = "0 * * ? * *";
        public static async Task StartAsync()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                var updateRatesJob = JobBuilder.Create<UpdateRatesJob>().WithIdentity("ExecuteTaskServiceCallUpdateRatesJob", "group1").Build();
                var updateRatesTrigger = TriggerBuilder.Create().WithIdentity("ExecuteTaskServiceCallTrigger1", "group1").WithCronSchedule(ScheduleCronExpression).Build();
                await scheduler.ScheduleJob(updateRatesJob, updateRatesTrigger);
            }
            catch (Exception ex) { }
        }
    }
}
