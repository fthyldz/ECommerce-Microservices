using Catalog.Domain.Primitives;

namespace Catalog.Domain.Events.DomainEvents;

public record StockUpdatedDomainEvent(Guid ProductId, int Quantity, Guid CorrelationId) : IDomainEvent
{
    public Guid EventId { get; } = CorrelationId;
    public DateTime OccurredOn { get; } = DateTime.Now;
    public string EventName { get; } = typeof(StockUpdatedDomainEvent).AssemblyQualifiedName!;   
}