using Catalog.Domain.Events;
using ECommerce.Application.Abstractions.Api.Common;
using ECommerce.Domain.Primitives;
using ECommerce.MessageContracts.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace Catalog.Application.Products.EventHandlers;

public class ProductCreatedDomainEventHandler(IPublishEndpoint publishEndpoint, ILogger<ProductCreatedDomainEventHandler> logger, AsyncPolicy asyncPolicy, ICorrelationIdService correlationIdService) : INotificationHandler<ProductCreatedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        IDomainEvent eventInfo = domainEvent;
        var correlationId = correlationIdService.GetCorrelationId();

        logger.LogInformation("[START] Handling domain event: {DomainEvent} - CorrelationId: {CorrelationId} - DomainEventId: {DomainEventId} - EventData: {@EventData}", nameof(ProductCreatedDomainEvent), correlationId, eventInfo.EventId, domainEvent);        
        
        var productCreatedEvent = ProductCreatedEvent.Create(
            correlationId,
            eventInfo.EventId,
            domainEvent.Product.Id,
            domainEvent.Product.Name,
            domainEvent.Product.Price,
            domainEvent.Product.Stock.Quantity);
        
        await asyncPolicy.ExecuteAsync(async () => await publishEndpoint.Publish(productCreatedEvent, cancellationToken));

        logger.LogInformation("[END] Handled domain event: {DomainEvent} - CorrelationId: {CorrelationId} - DomainEventId: {DomainEventId}", nameof(ProductCreatedDomainEvent), correlationId, eventInfo.EventId);
    }
}