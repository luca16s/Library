namespace Core.Events;

using Core.Events.Interfaces.Event;
using Core.Models;

using System.Collections.Generic;

public abstract class AggregateEventSourcing<TId> : Entity<TId>, IAggregateEventSourcing<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IDomainEvent[] ClearDomainEvents()
    {
        var dequeuedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeuedEvents;
    }

    public virtual void When(object @event) { }
}
