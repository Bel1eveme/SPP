namespace FifthLab.ParallelWaitAll;

public static class ParallelAllWaiter
{
    public static void DoAllTasks<T>(List<Action<T>> actions, List<T> arguments)
    {
        if (arguments.Count != actions.Count)
        {
            throw new ArgumentException("Different number of actions and arguments.");
        }

        Task.WaitAll(actions.Select((t, i) => Task.Run(() => t(arguments[i]))).ToArray());
    }
}