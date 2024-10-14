namespace Core.Events.Interfaces.Event;

using MassTransit;

using MediatR;

using System;

public interface IEvent : INotification
{
    Guid EventId => NewId.NextGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string? EventType => GetType()?.AssemblyQualifiedName;
}
