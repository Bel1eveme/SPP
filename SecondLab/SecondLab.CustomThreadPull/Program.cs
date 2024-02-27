using SecondLab.CustomThreadPull;

Console.WriteLine("Hello, World!");


TaskQueue<string> queue = new(3);

List<Action<string>> tasks =
[
    Console.WriteLine,
    Console.WriteLine,
    Console.WriteLine,
    Console.WriteLine,
    Console.WriteLine,
    Console.WriteLine,
];

List<string> arguments =
[
    "Task1",
    "Task2",
    "Task3",
    "Task4",
    "Task5",
    "Task6",
];

for (int i = 0; i < tasks.Count; i++)
{
    queue.AddTask(tasks[i], arguments[i]);
}

queue.EndAllTasks();

Console.WriteLine();