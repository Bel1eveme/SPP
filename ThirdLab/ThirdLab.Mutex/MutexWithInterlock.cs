namespace ThirdLab.Mutex;

public class MutexWithInterlock
{
    private int _lockValue = 0;

    private readonly EventWaitHandle _eventWaitHandle = new(false, EventResetMode.AutoReset);
    
    public void Lock()
    {
        if (Interlocked.Exchange(ref _lockValue, 1) == 1)
        {
            _eventWaitHandle.WaitOne();
        }
    }

    public void Unlock()
    {
        _eventWaitHandle.Set();
        Interlocked.Exchange(ref _lockValue, 0);
    }
}