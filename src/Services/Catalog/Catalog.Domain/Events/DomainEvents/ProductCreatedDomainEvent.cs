using Catalog.Domain.Entities;
using Catalog.Domain.Primitives;

namespace Catalog.Domain.Events.DomainEvents;

public record ProductCreatedDomainEvent(Product Product) : IDomainEvent;