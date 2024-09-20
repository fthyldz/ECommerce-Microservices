using ECommerce.MessageBus.Events;

namespace ECommerce.MessageBus.Models;

public record OrderCreated : IOrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public List<IOrderCreatedItem> OrderItems { get; set; }
    public Guid CorrelationId { get; set; }
}

public record OrderCreatedItem : IOrderCreatedItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}