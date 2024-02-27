using System.Collections.Concurrent;
using SecondLab.FileCopier.Exceptions;

namespace SecondLab.FileCopier;

public class FileCopier
{
    public int CopiedFilesCount => _copiedFilesCount;
    public List<string> ErrorMessages => _errorMessages.ToList();

    private readonly CopyOptions _copyOptions;

    private readonly ConcurrentBag<string> _errorMessages;

    private string _sourceFolder;
    
    private string _destinationFolder;

    private List<string> _files;

    private int _copiedFilesCount;
    
    public FileCopier(CopyOptions copyOptions)
    {
        _copyOptions = copyOptions;
        _files = new List<string>();
        _errorMessages = new ConcurrentBag<string>();
        _copiedFilesCount = 0;
        _sourceFolder = string.Empty;
        _destinationFolder = string.Empty;
    }

    public void CopyFiles(string sourceFolder, string destinationFolder)
    {
        _sourceFolder = sourceFolder;
        _destinationFolder = destinationFolder;
        
        _files = GetFileList(sourceFolder);
        
        var directoryManager = new DirectoryManager(_files, sourceFolder, destinationFolder);
        
        CreateFolderIfNotExists(_destinationFolder);
        directoryManager.CreateDirectoriesForFiles();
        
        CopyFilesMultithreading();
    }

    private List<string> GetFileList(string rootFolderPath)
    {
        try
        {
            return Directory.GetFiles(rootFolderPath, "*", _copyOptions.SearchOption).ToList();
        }
        catch (Exception exception) when (exception is ArgumentException
                                              or IOException
                                              or UnauthorizedAccessException
                                              or PathTooLongException
                                              or DirectoryNotFoundException) 
        {
            throw new FileSearchException(exception.Message);
        }
    }

    private void CopyFilesMultithreading()
    {
        foreach (var file in _files)
        {
            var newFilePath = Path.Combine(_destinationFolder, Path.GetRelativePath(_sourceFolder, file));
            
            ThreadPool.QueueUserWorkItem(_ => CopySingleFile(file, newFilePath, _copyOptions.DoOverWrite));
        }

        while (ThreadPool.PendingWorkItemCount != 0) { }
        
        if (_errorMessages.IsEmpty) return;
        
        foreach (var message in _errorMessages)
        {
            Console.WriteLine(message);
        }
            
        throw new FileCopyException(string.Join(Environment.NewLine, _errorMessages));
    }

    private void CopySingleFile(string sourceFilePath, string destinationFilePath, bool doOverwrite)
    {
        try
        {
            File.Copy(sourceFilePath, destinationFilePath, doOverwrite);

            Interlocked.Increment(ref _copiedFilesCount);
        }
        catch (Exception exception) when (exception is UnauthorizedAccessException
                                              or DirectoryNotFoundException
                                              or FileNotFoundException
                                              or IOException)
        {
            _errorMessages.Add($"{sourceFilePath}: {exception.Message}");
        }
    }
    
    private void CreateFolderIfNotExists(string folder)
    {
        if (Directory.Exists(folder)) return;
        
        try
        {
            Directory.CreateDirectory(folder);
        }
        catch (Exception exception)
        {
            _errorMessages.Add(exception.Message);
        }
    }
}