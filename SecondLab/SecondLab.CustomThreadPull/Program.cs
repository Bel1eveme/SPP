using SecondLab.CustomThreadPull;

Console.WriteLine("Hello, World!");


TaskQueue queue = new(3);

List<Action> tasks =
[
    () => Console.WriteLine("Task1"),
    () => Console.WriteLine("Task2"),
    () => Console.WriteLine("Task3"),
    () => Console.WriteLine("Task4"),
    () => Console.WriteLine("Task5"),
    () => Console.WriteLine("Task6")
];

foreach (var task in tasks)
{
    queue.AddTask(task);
}

queue.Dispose();

Console.WriteLine();