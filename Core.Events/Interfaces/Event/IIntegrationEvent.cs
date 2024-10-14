namespace Core.Events.Interfaces.Event;

using MassTransit;

[ExcludeFromTopology]
public interface IIntegrationEvent : IEvent
{ }
