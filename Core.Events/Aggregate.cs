namespace Core.Events;

using Core.Events.Interfaces.Event;
using Core.Interfaces;
using Core.Models;

using System.Collections.Generic;

public abstract class Aggregate<TId>(
    TId Id
) : Entity<TId>(Id), IAggregate<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IEvent[] ClearDomainEvents()
    {
        IEvent[] dequeuedEvents = [.. _domainEvents];

        _domainEvents.Clear();

        return dequeuedEvents;
    }
}
