namespace Core.Events;

using Core.Events.Interfaces.Event;

using System.Collections.Generic;

public abstract class AggregateEventSourcing<TId> : Aggregate<TId>, IAggregateEventSourcing<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public virtual void When(object @event) { }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeuedEvents = [.. _domainEvents];

        _domainEvents.Clear();

        return dequeuedEvents;
    }
}
