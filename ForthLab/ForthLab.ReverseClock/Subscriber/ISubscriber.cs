using ForthLab.ReverseClock.Notifier;

namespace ForthLab.ReverseClock.Subscriber;

public interface ISubscriber
{
    public void Update<T>(T message);
}