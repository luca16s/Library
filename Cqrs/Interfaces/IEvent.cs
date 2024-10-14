namespace Cqrs.Interfaces;
using MediatR;

using System;

public interface IEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string? EventType => GetType()?.AssemblyQualifiedName;
}
