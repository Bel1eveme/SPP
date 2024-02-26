namespace SecondLab.FileCopier.Exceptions;

public class FileCopierException : ApplicationException
{
    public FileCopierException()
    {
    }

    public FileCopierException(string message)
        : base(message)
    {
    }

    public FileCopierException(string message, Exception inner)
        : base(message, inner)
    {
    }
}