namespace ForthLab.ReverseClock;

public class MessageService
{
    private Task? _timerTask;
    
    private readonly PeriodicTimer _timer;

    private readonly CancellationTokenSource _cancellationTokenSource;

    public MessageService(TimeSpan interval)
    {
        _timer = new PeriodicTimer(interval);

        _cancellationTokenSource = new();
    }

    public void Start(Action operationDelegate)
    {
        _timerTask = DoWorkAsync(operationDelegate);
    }

    private async Task DoWorkAsync(Action operationDelegate)
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(_cancellationTokenSource.Token))
            {
                operationDelegate.Invoke();
            }
        }
        catch (OperationCanceledException)
        {
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
        
        Console.WriteLine("Task was canceled");
    }
}