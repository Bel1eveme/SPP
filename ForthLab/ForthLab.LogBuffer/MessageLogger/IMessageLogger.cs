namespace ForthLab.LogBuffer.MessageLogger;

public interface IMessageLogger
{
    public Task AddMessage(string message);

    public Task Flush();
}