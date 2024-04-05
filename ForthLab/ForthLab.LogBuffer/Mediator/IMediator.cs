namespace ForthLab.LogBuffer.Mediator;

public interface IMediator
{
    Task Notify(object sender, string ev);
}