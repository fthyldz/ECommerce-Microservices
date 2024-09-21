using ECommerce.MessageContracts.Events;

namespace ECommerce.MessageContracts.Models;

public record OrderCreatedEvent : IOrderCreatedEvent
{
    public Guid CorrelationId { get; set; }
    public Guid EventId { get; set; }
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public List<IOrderCreatedItem> OrderItems { get; set; }
    
    public static IOrderCreatedEvent Create(Guid correlationId, Guid eventId, Guid orderId, string customerName, string address, List<IOrderCreatedItem> orderItems)
    {
        return new OrderCreatedEvent
        {
            CorrelationId = correlationId,
            EventId = eventId,
            OrderId = orderId,
            CustomerName = customerName,
            Address = address,
            OrderItems = orderItems
        };
    }
}

public record OrderCreatedItem : IOrderCreatedItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}