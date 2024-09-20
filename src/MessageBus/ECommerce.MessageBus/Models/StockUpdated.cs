using ECommerce.MessageBus.Events;

namespace ECommerce.MessageBus.Models;

public record StockUpdated : IStockUpdatedEvent
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid CorrelationId { get; set; }
}