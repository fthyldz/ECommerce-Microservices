using Catalog.Domain.Events;
using ECommerce.Domain.Primitives;
using ECommerce.MessageContracts.Events;
using ECommerce.MessageContracts.Models;
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
        IDomainEvent eventInfo = domainEvent;
        logger.LogInformation("[START] Handle Request={Request} - CorrelationId={CorrelationId} - RequestData={RequestData}",
            nameof(StockUpdatedDomainEvent), eventInfo.EventId, domainEvent);
        
        var stockUpdatedEvent = new StockUpdatedEvent { CorrelationId = eventInfo.EventId, ProductId = domainEvent.ProductId, Quantity = domainEvent.Quantity };
        await publishEndpoint.Publish<IStockUpdatedEvent>(stockUpdatedEvent, cancellationToken);
        
        logger.LogInformation("[END] Handled {Request}", nameof(StockUpdatedDomainEvent));
    }
}