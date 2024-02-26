namespace SecondLab.FileCopier.Exceptions;

public class FileSearchException : FileCopierException
{
    public FileSearchException()
    {
    }

    public FileSearchException(string message)
        : base(message)
    {
    }

    public FileSearchException(string message, Exception inner)
        : base(message, inner)
    {
    }
}