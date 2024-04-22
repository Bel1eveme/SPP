using FifthLab.ExploreAttribute;

if (args.Length != 1)
{
    Console.WriteLine("Wrong argument count.");
    return;
}

//AssemblyScanner.PrintAllMarkedTypes("D:\\study\\SPP\\FifthLab\\FifthLab.ExploreAttribute\\assembly\\FifthLab.ExploreAttribute.dll", typeof(ExploreTypeAttribute));

AssemblyScanner.PrintAllMarkedTypes(args[0], typeof(ExploreTypeAttribute));