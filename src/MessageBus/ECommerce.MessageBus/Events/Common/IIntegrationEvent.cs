namespace ECommerce.MessageBus.Events.Common;

public interface IIntegrationEvent
{
    Guid CorrelationId { get; set; }
}