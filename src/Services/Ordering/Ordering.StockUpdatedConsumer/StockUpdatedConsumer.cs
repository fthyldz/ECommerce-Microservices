using ECommerce.MessageBus.Events;
using MassTransit;
using Polly;

namespace Ordering.StockUpdatedConsumer;

public class StockUpdatedEventConsumer : IConsumer<IStockUpdatedEvent>
{
    public async Task Consume(ConsumeContext<IStockUpdatedEvent> context)
    {
        var logger = context.GetServiceOrCreateInstance<ILogger<StockUpdatedEventConsumer>>();
        logger.LogInformation("[START] Handle Request={Request} - CorrelationId={CorrelationId} - RequestData={RequestData}",
            nameof(IStockUpdatedEvent), context.Message.CorrelationId, context.Message);

        var orderingApi = context.GetServiceOrCreateInstance<IOrderingApi>();
        var asyncPolicy = context.GetServiceOrCreateInstance<AsyncPolicy>();
        
        var message = context.Message;

        await asyncPolicy.ExecuteAsync(() => orderingApi.UpdateStock(message));
        
        logger.LogInformation("[END] Handled {Request}", nameof(IStockUpdatedEvent));
    }
}