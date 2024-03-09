using ThirdLab.Mutex;

MutexWithInterlock mutex = new();

Thread thread1 = new (DoWork);
Thread thread2 = new (DoWork);

thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

return;


void DoWork()
{
    mutex.Lock();

    try
    {
        Console.WriteLine("Выполняется критическая секция кода");
        
        Thread.Sleep(3000);
    }
    finally
    {
        mutex.Unlock();
    }
}