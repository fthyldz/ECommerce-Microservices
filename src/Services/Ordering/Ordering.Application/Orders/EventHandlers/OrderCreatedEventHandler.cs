using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events.DomainEvents;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request={Request} - RequestData={RequestData}",
            nameof(OrderCreatedEvent), domainEvent);
        
        var orderCreatedEvent = new OrderCreated
        {
            CorrelationId = domainEvent.EventId,
            OrderId = domainEvent.Order.Id,
            CustomerName = domainEvent.Order.CustomerName,
            Address = domainEvent.Order.Address,
            OrderItems = domainEvent.Order.OrderItems.Select(IOrderCreatedItem (orderItem) => new OrderCreatedItem
            {
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            }).ToList()
        };
        await publishEndpoint.Publish<IOrderCreatedEvent>(orderCreatedEvent, cancellationToken);
        
        logger.LogInformation("{Event} fırlatıldı. CorrelationId: {CorrelationId}", nameof(OrderCreatedEvent), orderCreatedEvent.CorrelationId);
        
        logger.LogInformation("[END] Handled {Request}", nameof(OrderCreatedEvent));
    }
}