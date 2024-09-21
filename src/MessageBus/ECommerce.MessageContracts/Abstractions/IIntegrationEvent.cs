namespace ECommerce.MessageContracts.Abstractions;

public interface IIntegrationEvent
{
    Guid CorrelationId { get; set; }
    Guid EventId { get; set; }

    public DateTime OccurredOn => DateTime.Now;

    public string EventName => GetType().AssemblyQualifiedName!;
}