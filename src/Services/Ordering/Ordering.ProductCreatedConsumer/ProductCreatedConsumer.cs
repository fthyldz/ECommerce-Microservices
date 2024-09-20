using ECommerce.MessageBus.Events;
using MassTransit;

namespace Ordering.ProductCreatedConsumer;

public class ProductCreatedEventConsumer : IConsumer<IProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<IProductCreatedEvent> context)
    {
        var orderingApi = context.GetServiceOrCreateInstance<IOrderingApi>();
        
        var message = context.Message;

        await orderingApi.CreateProduct(message);
    }
}