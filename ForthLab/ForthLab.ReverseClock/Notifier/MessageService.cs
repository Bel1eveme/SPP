namespace ForthLab.ReverseClock.Notifier;

public class MessageService
{
    private Task? _timerTask;
    
    private readonly PeriodicTimer _timer;

    private readonly CancellationTokenSource _cancellationTokenSource;

    private readonly INotifier _notifier;

    public MessageService(TimeSpan interval, INotifier notifier)
    {
        _notifier = notifier;
        _timer = new PeriodicTimer(interval);

        _cancellationTokenSource = new();
    }

    public void Start()
    {
        Console.WriteLine("Service started");
        
        _timerTask = DoWorkAsync();
    }

    private async Task DoWorkAsync()
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(_cancellationTokenSource.Token))
            {
                _notifier.Notify();
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine(nameof(OperationCanceledException));
        }
    }

    public async Task StopAsync()
    {
        if (_timerTask is null)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        await _timerTask;
        _cancellationTokenSource.Dispose();
        
        Console.WriteLine("Server canceled");
    }
}