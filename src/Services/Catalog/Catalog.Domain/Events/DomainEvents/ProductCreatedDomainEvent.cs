using Catalog.Domain.Entities;
using Catalog.Domain.Primitives;

namespace Catalog.Domain.Events.DomainEvents;

public record ProductCreatedDomainEvent(Product Product) : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName!;
}