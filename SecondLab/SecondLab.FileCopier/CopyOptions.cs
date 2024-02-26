namespace SecondLab.FileCopier;

public class CopyOptions(bool doOverWrite, SearchOption searchOption)
{
    public readonly bool DoOverWrite = doOverWrite;

    public readonly SearchOption SearchOption = searchOption;
}