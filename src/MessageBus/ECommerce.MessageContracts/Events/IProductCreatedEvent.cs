using ECommerce.MessageContracts.Abstractions;

namespace ECommerce.MessageContracts.Events;

public interface IProductCreatedEvent : IIntegrationEvent
{
    Guid ProductId { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
}