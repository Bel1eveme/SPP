namespace SecondLab.CustomThreadPull;

public class TaskQueue<T>
{
    private readonly List<Thread> _threads;

    private readonly Queue<Action<T>> _tasks = new();

    private readonly Queue<T> _arguments = new();
    
    private readonly object _locker = new();

    // ReSharper disable once RedundantDefaultMemberInitializer
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
    
    public void AddTask(Action<T> task, T argument)
    {
        lock (_locker)
        {
            _tasks.Enqueue(task);
            _arguments.Enqueue(argument);
            
            Monitor.PulseAll(_locker);
        }
    }

    private void ThreadPendingAction()
    {
        while (true)
        {
            Action<T> item;
            T argument;
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
                argument = _arguments.Dequeue();
            }
            
            item.Invoke(argument);
        }
    }

    public void EndAllTasks()
    {
        _isStop = true;

        _threads.ForEach(thread => thread.Join());
    }
    
}