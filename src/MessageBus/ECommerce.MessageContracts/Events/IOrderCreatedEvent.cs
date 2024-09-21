using ECommerce.MessageContracts.Abstractions;

namespace ECommerce.MessageContracts.Events;

public interface IOrderCreatedEvent : IIntegrationEvent
{
    Guid OrderId { get; set; }
    string CustomerName { get; set; }
    string Address { get; set; }
    List<IOrderCreatedItem> OrderItems { get; set; }
}

public interface IOrderCreatedItem
{
    Guid ProductId { get; set; }
    int Quantity { get; set; }
    decimal Price { get; set; }
}