using Catalog.Domain.Entities;
using ECommerce.Domain.Primitives;

namespace Catalog.Domain.Events;

public record ProductCreatedDomainEvent(Product Product) : IDomainEvent;