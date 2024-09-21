using ECommerce.Application.Abstractions.Api.Common;
using ECommerce.Domain.Primitives;
using ECommerce.MessageContracts.Events;
using ECommerce.MessageContracts.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger, ICorrelationIdService correlationIdService)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        IDomainEvent eventInfo = domainEvent;
        var correlationId = correlationIdService.GetCorrelationId();

        logger.LogInformation("[START] Handling domain event: {DomainEvent} - CorrelationId: {CorrelationId} - DomainEventId: {DomainEventId} - EventData: {@EventData}", nameof(OrderCreatedDomainEvent), correlationId, eventInfo.EventId, domainEvent);

        var orderCreatedEvent = OrderCreatedEvent.Create(
            correlationId,
            eventInfo.EventId,
            domainEvent.Order.Id,
            domainEvent.Order.CustomerName,
            domainEvent.Order.Address,
            domainEvent.Order.OrderItems.Select(IOrderCreatedItem (orderItem) => new OrderCreatedItem
            {
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            }).ToList());
        
        await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
        
        logger.LogInformation("[END] Handled domain event: {DomainEvent} - CorrelationId: {CorrelationId} - DomainEventId: {DomainEventId}", nameof(OrderCreatedDomainEvent), correlationId, eventInfo.EventId);
    }
}