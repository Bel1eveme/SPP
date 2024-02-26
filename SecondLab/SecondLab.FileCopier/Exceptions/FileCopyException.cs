namespace SecondLab.FileCopier.Exceptions;

public class FileCopyException : FileCopierException
{
    public FileCopyException()
    {
    }

    public FileCopyException(string message)
        : base(message)
    {
    }

    public FileCopyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}