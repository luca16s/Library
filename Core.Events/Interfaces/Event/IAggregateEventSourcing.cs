namespace Core.Events.Interfaces.Event;

using Core.Interfaces;

using System.Collections.Generic;

public interface IAggregateEventSourcing : IProjection, IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}

public interface IAggregateEventSourcing<TId> : IAggregateEventSourcing, IEntity<TId>
{ }
