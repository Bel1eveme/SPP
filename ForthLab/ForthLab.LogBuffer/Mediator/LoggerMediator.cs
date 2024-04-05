namespace ForthLab.LogBuffer.Mediator;

public class LoggerMediator : IMediator
{
    public async Task Notify(object sender, string ev)
    {
        if (sender is Logger logBuffer)
        {
            if (ev == "messageCountReached")
            {
                await logBuffer.Flush();
            }
        }
    }
}