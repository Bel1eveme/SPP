using ForthLab.LogBuffer.Scheduler;
using Quartz;

using ForthLab.LogBuffer.Scheduler.SchedulerFactory;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<ICustomSchedulerFactory, CustomSchedulerFactory>();
services.AddQuartz(q =>
    {
        var jobKey = new JobKey("FlushJob");
        q.AddJob<FlushJob>(opts => opts.WithIdentity(jobKey));
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("FlushJob-trigger")
            .StartNow()
            .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever()));
    });
    

var serviceProvider = services.BuildServiceProvider();

