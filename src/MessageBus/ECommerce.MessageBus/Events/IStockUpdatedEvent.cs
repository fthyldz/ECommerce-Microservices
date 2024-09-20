using ECommerce.MessageBus.Events.Common;

namespace ECommerce.MessageBus.Events;

public interface IStockUpdatedEvent : IIntegrationEvent
{
    Guid ProductId { get; set; }
    int Quantity { get; set; }
}