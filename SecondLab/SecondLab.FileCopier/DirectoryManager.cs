using System.Collections.Concurrent;

namespace SecondLab.FileCopier;

public class DirectoryManager
{
    public List<string> ErrorMessages => _errorMessages.ToList();
    
    private readonly List<string> _files;
    
    private readonly string _sourceFolder;
    
    private readonly string _destinationFolder;

    private readonly ConcurrentDictionary<string, int> _directories;
    
    private readonly ConcurrentBag<string> _errorMessages;
    
    public DirectoryManager(List<string> files, string sourceFolder, string destinationFolder)
    {
        _files = files;
        _sourceFolder = sourceFolder;
        _destinationFolder = destinationFolder;
        _directories = new ConcurrentDictionary<string, int>();
        _errorMessages = new ConcurrentBag<string>();
    }

    public void CreateDirectoriesForFiles()
    {
        foreach (var file in _files)
        {
            ThreadPool.QueueUserWorkItem(_ => CreateDirectory(Path.GetDirectoryName(file)!));
        }
    }

    private void CreateDirectory(string directoryPath)
    {
        try
        {
            var newPath = Path.Combine(_destinationFolder, Path.GetRelativePath(_sourceFolder, directoryPath));
            
            if (_directories.ContainsKey(newPath)) return;
            
            Directory.CreateDirectory(newPath);
            _directories.TryAdd(newPath, 1);
        }
        catch (Exception exception) when (exception is ArgumentException
                                              or IOException
                                              or UnauthorizedAccessException
                                              or PathTooLongException
                                              or DirectoryNotFoundException
                                              or ArgumentNullException)
        {
            _errorMessages.Add(exception.Message);
        }
    }

}