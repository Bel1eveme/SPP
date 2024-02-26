using Cocona;
using SecondLab.FileCopier;
using SecondLab.FileCopier.Exceptions;

var app = CoconaApp.Create();

app.AddCommand("info", () => Console.WriteLine("Show information"));

app.AddCommand("copy", (string sourcePath, string destinationPath) =>
{
    Console.WriteLine($"{sourcePath} -> {destinationPath}");

    CopyOptions options = new(true, SearchOption.AllDirectories);
    FileCopier copier = new (options);

    try
    {
        copier.CopyFiles(sourcePath, destinationPath);
    }
    catch (FileCopierException exception)
    {
        Console.WriteLine(exception.Message);
    }
    
    
    Console.WriteLine($"{copier.CopiedFilesCount} files had been copied.");
});



app.Run();