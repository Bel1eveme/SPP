namespace ForthLab.ReverseClock;

public class MessageNotifier : INotifier
{
    private readonly MessageService _messageService;
    
    public MessageNotifier(TimeSpan interval)
    {
        _subscribers = [];

        _messageService = new MessageService(interval);
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

    private void NotifySubscribers()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.GetMessage(Console.WriteLine, "hello");
        }
    }

    public void Start()
    {
        _messageService.Start(NotifySubscribers);
    }
    
    public async void Stop()
    {
        await _messageService.StopAsync();
    }
}