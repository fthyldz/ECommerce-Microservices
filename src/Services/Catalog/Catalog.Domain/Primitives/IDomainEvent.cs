using MediatR;

namespace Catalog.Domain.Primitives;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName!;
}