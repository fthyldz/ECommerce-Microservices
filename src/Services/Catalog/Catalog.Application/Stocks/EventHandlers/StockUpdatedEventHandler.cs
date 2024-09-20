using Catalog.Domain.Events.DomainEvents;
using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Stocks.EventHandlers;

public class StockUpdatedEventHandler(
    IPublishEndpoint publishEndpoint, ILogger<StockUpdatedEventHandler> logger)
    : INotificationHandler<StockUpdatedDomainEvent>
{
    public async Task Handle(StockUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request={Request} - CorrelationId={CorrelationId} - RequestData={RequestData}",
            nameof(StockUpdatedDomainEvent), domainEvent.EventId, domainEvent);
        
        var stockUpdatedEvent = new StockUpdated { CorrelationId = domainEvent.EventId, ProductId = domainEvent.ProductId, Quantity = domainEvent.Quantity };
        await publishEndpoint.Publish<IStockUpdatedEvent>(stockUpdatedEvent, cancellationToken);
        
        logger.LogInformation("[END] Handled {Request}", nameof(StockUpdatedDomainEvent));
    }
}