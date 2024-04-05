namespace ForthLab.ReverseClock;

public interface ISubscriber
{
    public void GetMessage<T>(Action<T>? handler, T parameter);

    public void Subscribe(INotifier notifier);
    
    public void Unsubscribe();
}