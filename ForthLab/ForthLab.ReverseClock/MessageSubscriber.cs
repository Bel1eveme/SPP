namespace ForthLab.ReverseClock;

public class MessageSubscriber : ISubscriber
{
    private INotifier? _notifier;
    
    public void GetMessage<T>(Action<T>? handler, T parameter)
    {
        handler?.Invoke(parameter);
    }

    public void Subscribe(INotifier notifier)
    {
        notifier.AddSubscriber(this);
        
        _notifier = notifier;
    }

    public void Unsubscribe()
    {
        _notifier?.RemoveSubscriber(this);
    }
}