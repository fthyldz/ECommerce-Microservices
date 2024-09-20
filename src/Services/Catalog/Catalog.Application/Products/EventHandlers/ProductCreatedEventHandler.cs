using Catalog.Domain.Events.DomainEvents;
using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Products.EventHandlers;

public class ProductCreatedEventHandler(
    IPublishEndpoint publishEndpoint, ILogger<ProductCreatedEventHandler> logger)
    : INotificationHandler<ProductCreatedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request={Request} - CorrelationId={CorrelationId} - RequestData={RequestData}",
            nameof(ProductCreatedDomainEvent), domainEvent.EventId, domainEvent);
        
        var productCreatedEvent = new ProductCreated { CorrelationId = domainEvent.EventId, ProductId = domainEvent.Product.Id, Name = domainEvent.Product.Name, Price = domainEvent.Product.Price, Quantity = domainEvent.Product.Stock.Quantity };
        await publishEndpoint.Publish<IProductCreatedEvent>(productCreatedEvent, cancellationToken);
        
        logger.LogInformation("[END] Handled {Request}", nameof(ProductCreatedDomainEvent));
    }
}