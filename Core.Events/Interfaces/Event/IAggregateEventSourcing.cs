namespace Core.Events.Interfaces.Event;

using Core.Interfaces;

using System.Collections.Generic;

public interface IAggregateEventSourcing : IProjection, IEntity
{
    IDomainEvent[] ClearDomainEvents();
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
}

public interface IAggregateEventSourcing<TId> : IAggregateEventSourcing, IAggregate<TId>
{ }
