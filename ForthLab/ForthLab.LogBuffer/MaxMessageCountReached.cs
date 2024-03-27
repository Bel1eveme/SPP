using Coravel.Events.Interfaces;

namespace ForthLab.LogBuffer;

public class MaxMessageCountReached(LogBuffer logBuffer) : IEvent
{
    private readonly LogBuffer _logBuffer = logBuffer;
}