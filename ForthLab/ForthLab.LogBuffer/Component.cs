using ForthLab.LogBuffer.Mediator;

namespace ForthLab.LogBuffer;

public class Component
{
    protected IMediator Mediator;

    public Component(IMediator mediator)
    {
        Mediator = mediator;
    }

    public void SetMediator(IMediator mediator)
    {
        Mediator = mediator;
    }
}