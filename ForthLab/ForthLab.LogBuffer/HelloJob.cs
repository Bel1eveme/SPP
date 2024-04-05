using Quartz;

namespace ForthLab.LogBuffer;

public class HelloJob : IJob
{
    private int _shouldRun = 0;
    
    public async Task Execute(IJobExecutionContext context)
    {
        /*if (Interlocked.CompareExchange(ref _shouldRun, 0, 1) == 1)
        {
            // The original value was 1, so we proceed with the execution.
            // Perform job tasks...

            // To reset and allow the job to run next time, you would later set shouldRun back to 1
            // potentially at the end of this method or from somewhere else in the application.
        }
        else
        {
            // The job was set to not run, so return immediately.
            return Task.CompletedTask;
        }*/
        
        await Console.Out.WriteLineAsync("Greetings from HelloJob!");
    }
}