namespace Core.Events.Interfaces.Event;
public interface IProjection
{
    void When(object @event);
}
