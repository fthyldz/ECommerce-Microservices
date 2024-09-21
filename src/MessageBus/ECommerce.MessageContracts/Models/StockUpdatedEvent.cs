using ECommerce.MessageContracts.Events;

namespace ECommerce.MessageContracts.Models;

public record StockUpdatedEvent : IStockUpdatedEvent
{
    public Guid CorrelationId { get; set; }
    public Guid EventId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}