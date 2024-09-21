using Ordering.Domain.Entities;
using ECommerce.Domain.Primitives;

namespace Ordering.Domain.Events;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName!;
}