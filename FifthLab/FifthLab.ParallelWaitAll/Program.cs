using FifthLab.ParallelWaitAll;

var actions = new List<Action<int>>
{
    x => Console.WriteLine($"{x} ^ 6 = {double.Pow(x, 6)}"),
    x => Console.WriteLine($"{x} ^ 3 = {double.Pow(x, 3)}"),
    x => Console.WriteLine($"{x} ^ 2 = {double.Pow(x, 2)}"),
};

var arguments = new List<int>
{
    2, 4, 8
};

ParallelAllWaiter.DoAllTasks(actions, arguments);