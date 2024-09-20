using ECommerce.MessageBus.Dtos;
using ECommerce.MessageBus.Events;
using MassTransit;
using Polly;

namespace Catalog.OrderCreatedConsumer;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var logger = context.GetServiceOrCreateInstance<ILogger<OrderCreatedEventConsumer>>();
        logger.LogInformation("[START] Handle Request={Request} - RequestData={RequestData}",
            nameof(IOrderCreatedEvent), context.Message);
        
        var message = context.Message;
        
        logger.LogInformation("{Event} yakalandı. CorrelationId: {CorrelationId}", nameof(IOrderCreatedEvent), message.CorrelationId);

        var catalogApi = context.GetServiceOrCreateInstance<ICatalogApi>();
        var asyncPolicy = context.GetServiceOrCreateInstance<AsyncPolicy>();

        foreach (var orderItem in message.OrderItems)
        {
            var result = await asyncPolicy.ExecuteAsync(() => catalogApi.DecreaseStock(new DecreaseStockRequestDto(orderItem.ProductId, orderItem.Quantity, message.CorrelationId)));
            if (result)
                logger.LogInformation("Stock decreased for ProductId: {ProductId}, Quantity: {Quantity}", orderItem.ProductId, orderItem.Quantity);
        }
        
        logger.LogInformation("[END] Handled {Request}", nameof(IOrderCreatedEvent));
    }
}