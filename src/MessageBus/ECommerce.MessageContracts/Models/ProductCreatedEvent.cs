using ECommerce.MessageContracts.Events;

namespace ECommerce.MessageContracts.Models;

public class ProductCreatedEvent : IProductCreatedEvent
{
    public Guid CorrelationId { get; set; }
    public Guid EventId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public static IProductCreatedEvent Create(Guid correlationId, Guid eventId, Guid productId, string name, decimal price, int quantity)
    {
        return new ProductCreatedEvent
        {
            CorrelationId = correlationId,
            EventId = eventId,
            ProductId = productId,
            Name = name,
            Price = price,
            Quantity = quantity
        };
    }
}