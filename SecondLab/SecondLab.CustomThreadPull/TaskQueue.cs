namespace SecondLab.CustomThreadPull;

public class TaskQueue
{
    private readonly List<Thread> _threads;

    private readonly Queue<Action> _tasks = new();
    
    private readonly object _locker = new();

    private bool _isStop = false;
    
    public TaskQueue(int threadCount)
    {
        _threads = new List<Thread>(threadCount);
        
        for (int i = 0; i < threadCount; i++)
        {
            Thread newThread = new (ThreadPendingAction) 
                { 
                    IsBackground = true,
                    Name = $"Thrad {i}"
                };
            
            _threads.Add(newThread);
            newThread.Start();
        }
    }
    
    public void AddTask(Action task)
    {
        lock (_locker)
        {
            _tasks.Enqueue(task);
            
            Monitor.PulseAll(_locker);
        }
    }

    private void ThreadPendingAction()
    {
        while (true)
        {
            Action item;
            lock (_locker)
            {
                while (_tasks.Count == 0)
                {
                    if (_isStop)
                    {
                        return;
                    }
                    
                    Monitor.Wait(_locker);
                }
                item = _tasks.Dequeue();
            }
            
            item.Invoke();
        }
    }
        
    public void Dispose()
    {
        _isStop = true;

        _threads.ForEach(thread => thread.Join());
    }
    
}