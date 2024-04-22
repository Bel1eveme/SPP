namespace ForthLab.ReverseClock.Subscriber;

public class MessageSubscriber : ISubscriber
{
    public void Update<T>(T message)
    {
        Console.WriteLine(message);
    }
}