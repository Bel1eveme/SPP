namespace ForthLab.LogBuffer.MessageLogger;

public record LoggerConfiguration(string FilePath, TimeSpan ResetTimeout, int ResetMessageCount);