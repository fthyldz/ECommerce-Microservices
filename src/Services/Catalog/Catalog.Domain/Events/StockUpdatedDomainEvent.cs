using ECommerce.Domain.Primitives;

namespace Catalog.Domain.Events;

public record StockUpdatedDomainEvent(Guid ProductId, int Quantity) : IDomainEvent;