namespace Core.Events;

using Core.Events.Interfaces.Event;
using Core.Interfaces;

using System.Collections.Generic;

public abstract class Aggregate<TId> : IAggregate<TId>
    where TId : notnull
{
    private int? _requestedHashCode;

    private readonly List<IDomainEvent> _domainEvents = [];

    protected Aggregate()
    {
        if (Id is null)
            throw new ArgumentNullException(nameof(Id));
    }

    protected Aggregate(TId id) => Id = id;

    /// <summary>
    /// Obtém identificador da entidade.
    /// </summary>
    public TId Id { get; set; }
    public long Version { get; set; }
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Gera o hash para a entidade.
    /// </summary>
    /// <returns>
    /// Hash da entidade.
    /// </returns>
    public override int GetHashCode()
    {
        if (!_requestedHashCode.HasValue)
            _requestedHashCode = Id.GetHashCode() ^ 31;

        return _requestedHashCode.Value;
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
