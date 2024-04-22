using ForthLab.ReverseClock.Subscriber;

namespace ForthLab.ReverseClock.Notifier;

public class MessageNotifier : INotifier
{
    public MessageNotifier()
    {
        _subscribers = [];
    }
    
    private readonly List<ISubscriber> _subscribers;
        
    public void AddSubscriber(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void Notify()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update("Message sent.");
        }
    }
}