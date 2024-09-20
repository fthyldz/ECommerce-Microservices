using Ordering.Domain.Entities;
using Ordering.Domain.Primitives;

namespace Ordering.Domain.Events.DomainEvents;

public record OrderCreatedEvent(Order Order) : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName!;
}