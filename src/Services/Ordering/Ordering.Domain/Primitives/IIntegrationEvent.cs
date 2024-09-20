using MediatR;

namespace Ordering.Domain.Primitives;

public interface IIntegrationEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName!;
}