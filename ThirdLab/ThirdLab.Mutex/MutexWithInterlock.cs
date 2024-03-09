namespace ThirdLab.Mutex;

public class MutexWithInterlock
{
    private int _lockValue = 0;
    
    public void Lock()
    {
        while (Interlocked.Exchange(ref _lockValue, 1) == 1)
        {
            Thread.Sleep(10);
        }
    }

    public void Unlock()
    {
        Interlocked.Exchange(ref _lockValue, 0);
    }
}