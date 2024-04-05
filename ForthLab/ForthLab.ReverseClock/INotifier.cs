namespace ForthLab.ReverseClock;

public interface INotifier
{
    public void AddSubscriber(ISubscriber subscriber);

    public void RemoveSubscriber(ISubscriber subscriber);
}