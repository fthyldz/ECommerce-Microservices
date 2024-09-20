using ECommerce.MessageBus.Events;
using MassTransit;
using Polly;

namespace Ordering.ProductCreatedConsumer;

public class ProductCreatedEventConsumer : IConsumer<IProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<IProductCreatedEvent> context)
    {
        var logger = context.GetServiceOrCreateInstance<ILogger<ProductCreatedEventConsumer>>();
        logger.LogInformation("[START] Handle Request={Request} - CorrelationId={CorrelationId} - RequestData={RequestData}",
            nameof(IProductCreatedEvent), context.Message.CorrelationId, context.Message);

        var orderingApi = context.GetServiceOrCreateInstance<IOrderingApi>();
        var asyncPolicy = context.GetServiceOrCreateInstance<AsyncPolicy>();

        var message = context.Message;

        await asyncPolicy.ExecuteAsync(() => orderingApi.CreateProduct(message));
        
        logger.LogInformation("[END] Handled {Request}", nameof(IProductCreatedEvent));
    }
}