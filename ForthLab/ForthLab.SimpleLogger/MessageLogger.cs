using System.Collections.Concurrent;

namespace ForthLab.MessageLogger;

public class MessageLogger
{
    private readonly LoggerConfiguration _loggerConfiguration;

    private readonly ConcurrentQueue<string> _messages;

    private int _messageCount;
    
    public MessageLogger(LoggerConfiguration loggerConfiguration)
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
            await Flush();
            //await RestartScheduler();
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