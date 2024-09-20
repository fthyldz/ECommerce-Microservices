using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using MediatR;
using Ordering.Domain.Events.DomainEvents;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var orderCreatedEvent = new OrderCreated
        {
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
    }
}