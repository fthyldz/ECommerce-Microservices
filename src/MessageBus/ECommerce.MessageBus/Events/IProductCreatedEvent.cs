namespace ECommerce.MessageBus.Events;

public interface IProductCreatedEvent
{
    Guid ProductId { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
}