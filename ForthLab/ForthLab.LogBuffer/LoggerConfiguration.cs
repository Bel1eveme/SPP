namespace ForthLab.LogBuffer;

public record LoggerConfiguration(string FilePath, TimeSpan ResetTimeout, int ResetMessageCount);