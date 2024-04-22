using System.Collections.Concurrent;
using ForthLab.LogBuffer.Scheduler;
using ForthLab.LogBuffer.Scheduler.SchedulerFactory;
using Quartz;

namespace ForthLab.LogBuffer.MessageLogger;

public class MessageLogger : IMessageLogger
{
    private readonly LoggerConfiguration _loggerConfiguration;
    
    private readonly IScheduler _scheduler;

    private IJobDetail _job;
    
    private ITrigger _trigger;

    private readonly ConcurrentQueue<string> _messages;

    private int _messageCount;
    
    public MessageLogger(LoggerConfiguration loggerConfiguration, ICustomSchedulerFactory customSchedulerFactory)
    {
        _loggerConfiguration = loggerConfiguration;
        _scheduler = customSchedulerFactory.GetScheduler().GetAwaiter().GetResult();

        _messages = [];
        _messageCount = 0;
        
        _job = JobBuilder.Create<FlushJob>()
            .WithIdentity(jobName, groupName)
            .UsingJobData(jobName, action)
            .Build();
        
        _trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName)
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(_loggerConfiguration.ResetTimeout.Milliseconds)
                .RepeatForever())
            .Build();
        
        _scheduler.Start();
        _scheduler.ScheduleJob(_job, _trigger);
    }
    
    public async Task AddMessage(string message)
    {
        _messages.Enqueue(message);
        var currentMessageCount = Interlocked.Increment(ref _messageCount);
        
        if (currentMessageCount == _loggerConfiguration.ResetMessageCount)
        {
            await Flush();
            await RestartScheduler();
        }
    }

    public async Task Flush()
    {
        var messagesToDeleteCount = _loggerConfiguration.ResetMessageCount;
        List<string> messagesToDelete = [];

        for (int i = 0; i < messagesToDeleteCount; i++)
        {
            if (!_messages.TryDequeue(out var message))
            {
                throw new Exception();
            }
            
            messagesToDelete.Add(message);
        }
        
        await File.WriteAllLinesAsync(_loggerConfiguration.FilePath, messagesToDelete);
    }
    
    private async Task RestartScheduler()
    {
        var newTrigger = TriggerBuilder.Create()
            .WithIdentity(_trigger.Key.Name, _trigger.Key.Group)
            .ForJob(_job.Key)
            .WithSimpleSchedule(x => x
                .WithInterval(_loggerConfiguration.ResetTimeout)
                .RepeatForever())
            .StartNow()
            .Build();
        
        _trigger = newTrigger;
        await _scheduler.RescheduleJob(_trigger.Key, _trigger);
    }
}