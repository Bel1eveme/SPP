using System.Collections.Concurrent;
using ForthLab.LogBuffer.Mediator;
using Quartz;
using Quartz.Impl;

namespace ForthLab.LogBuffer;

public class Logger : Component
{
    private readonly LoggerConfiguration _loggerConfiguration;

    private readonly ConcurrentQueue<string> _messages;

    private int _messageCount;

    private readonly IScheduler _scheduler;

    public Logger(LoggerConfiguration loggerConfiguration) : base(new LoggerMediator())
    {
        _loggerConfiguration = loggerConfiguration;

        _messages = [];
        _messageCount = 0;
    }

    public async Task AddMessage(string message)
    {
        _messages.Enqueue(message);
        var currentMessageCount = Interlocked.Increment(ref _messageCount);
        
        if (currentMessageCount == _loggerConfiguration.ResetMessageCount)
        {
            await Mediator.Notify(this, "messageCountReached");
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
}