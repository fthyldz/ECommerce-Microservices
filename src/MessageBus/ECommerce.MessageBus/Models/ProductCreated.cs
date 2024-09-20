using ECommerce.MessageBus.Events;

namespace ECommerce.MessageBus.Models;

public record ProductCreated : IProductCreatedEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}