using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;

namespace ForthLab.LogBuffer;

public class LogBuffer
{

    public readonly LoggerConfiguration LoggerConfiguration;

    private readonly ConcurrentQueue<string> _messages;

    private int _messageCount;

    private readonly IHost serviceHost;

    public LogBuffer(LoggerConfiguration loggerConfiguration)
    {
        LoggerConfiguration = loggerConfiguration;

        _messages = [];
        _messageCount = 0;

        //serviceHost = Host
    }

    public async Task AddMessage(string message)
    {
        _messages.Enqueue(message);
        var currentMessageCount = Interlocked.Increment(ref _messageCount);
        
        if (currentMessageCount == LoggerConfiguration.ResetMessageCount)
        {
            await Flush();
            Interlocked.Add(ref _messageCount, -currentMessageCount);
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