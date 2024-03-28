using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

namespace ForthLab.LogBuffer;

public class LogBuffer
{

    public readonly LoggerConfiguration LoggerConfiguration;

    private readonly ConcurrentQueue<string> _messages;

    private int _messageCount;

    private readonly IScheduler _scheduler;

    public LogBuffer(LoggerConfiguration loggerConfiguration)
    {
        LoggerConfiguration = loggerConfiguration;

        _messages = [];
        _messageCount = 0;
        
        StdSchedulerFactory factory = new StdSchedulerFactory();
        _scheduler =  factory.GetScheduler().GetAwaiter().GetResult();
        
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
        
        _scheduler.pa
        
        _scheduler.Start();
        _scheduler.ScheduleJob(job, trigger);

        PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
    }

    public async Task AddMessage(string message)
    {
        _messages.Enqueue(message);
        var currentMessageCount = Interlocked.Increment(ref _messageCount);
        
        if (currentMessageCount == LoggerConfiguration.ResetMessageCount)
        {
            await Flush();
            Interlocked.Add(ref _messageCount, -currentMessageCount);
            _scheduler.
        }
    }

    private async Task Flush()
    {
        var messagesToDeleteCount = LoggerConfiguration.ResetMessageCount;
        List<string> messagesToDelete = [];

        for (int i = 0; i < messagesToDeleteCount; i++)
        {
            if (!_messages.TryDequeue(out var message))
            {
                throw new Exception();
            }
            
            messagesToDelete.Add(message);
        }
        
        await File.WriteAllLinesAsync(LoggerConfiguration.FilePath, messagesToDelete);
    }
}