using ECommerce.MessageBus.Events;

namespace ECommerce.MessageBus.Models;

public record ProductCreated : IProductCreatedEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid CorrelationId { get; set; }
}