using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

namespace ForthLab.LogBuffer.Scheduler;

public class ScheduledService
{
    public ScheduledService()
    {
        StdSchedulerFactory factory = new StdSchedulerFactory();
        
        IScheduler scheduler = factory.GetScheduler().GetAwaiter().GetResult();
        
        IJobDetail job = JobBuilder.Create<HelloJob>()
            .WithIdentity("job1", "group1")
            .Build();
        
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(6)
                .RepeatForever())
            .Build();
        
        scheduler.Start();
        scheduler.ScheduleJob(job, trigger);
    }
    
}