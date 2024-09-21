using ECommerce.MessageContracts.Abstractions;

namespace ECommerce.MessageContracts.Events;

public interface IStockUpdatedEvent : IIntegrationEvent
{
    Guid ProductId { get; set; }
    int Quantity { get; set; }
}