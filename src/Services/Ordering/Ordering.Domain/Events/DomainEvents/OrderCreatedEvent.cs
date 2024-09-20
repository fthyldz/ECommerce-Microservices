using Ordering.Domain.Entities;
using Ordering.Domain.Primitives;

namespace Ordering.Domain.Events.DomainEvents;

public record OrderCreatedEvent(Order Order) : IDomainEvent;