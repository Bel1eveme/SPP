using Quartz;

namespace ForthLab.LogBuffer.Scheduler;

public class FlushJob : IJob
{
    private readonly MessageLogger.MessageLogger _messageLogger;
    
    public FlushJob(MessageLogger.MessageLogger messageLogger)
    {
        _messageLogger = messageLogger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _messageLogger.Flush();
    }
}