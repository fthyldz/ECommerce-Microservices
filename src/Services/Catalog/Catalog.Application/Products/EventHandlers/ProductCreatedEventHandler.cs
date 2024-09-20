using Catalog.Domain.Events.DomainEvents;
using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using MediatR;

namespace Catalog.Application.Products.EventHandlers;

public class ProductCreatedEventHandler(
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<ProductCreatedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productCreatedEvent = new ProductCreated { ProductId = domainEvent.Product.Id, Name = domainEvent.Product.Name, Price = domainEvent.Product.Price };
        await publishEndpoint.Publish<IProductCreatedEvent>(productCreatedEvent, cancellationToken);
    }
}