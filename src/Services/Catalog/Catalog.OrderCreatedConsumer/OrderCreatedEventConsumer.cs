using ECommerce.MessageBus.Dtos;
using ECommerce.MessageBus.Events;
using MassTransit;

namespace Catalog.OrderCreatedConsumer;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var catalogApi = context.GetServiceOrCreateInstance<ICatalogApi>();

        var message = context.Message;

        foreach (var orderItem in message.OrderItems)
        {
            await catalogApi.DecreaseStock(new DecreaseStockRequestDto(orderItem.ProductId, orderItem.Quantity));
        }
    }
}