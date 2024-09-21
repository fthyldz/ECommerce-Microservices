using ECommerce.MessageContracts.Events;
using Mapster;
using MassTransit;
using Ordering.Consumers.Common.ApiClients;
using Ordering.Consumers.Common.Dtos;
using Polly;

namespace Ordering.ProductCreatedEventConsumer;

public class ProductCreatedEventConsumer(ILogger<ProductCreatedEventConsumer> logger, IOrderingApi orderingApi, AsyncPolicy asyncPolicy) : IConsumer<IProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<IProductCreatedEvent> context)
    {
        var message = context.Message;
        
        logger.LogInformation("Handling ProductCreatedEvent with CorrelationId: {CorrelationId}, EventId: {EventId}, and Message: {@Message}", message.CorrelationId, message.EventId, message);        
        var request = message.Adapt<CreateProductDto>();
        
        var result = await asyncPolicy.ExecuteAsync(() => orderingApi.CreateProductAsync(message.CorrelationId.ToString(), request));
        
        if (result)
            logger.LogInformation("Product created successfully with CorrelationId: {CorrelationId}", message.CorrelationId);
        else
            logger.LogError("Failed to create product with CorrelationId: {CorrelationId}", message.CorrelationId);
        
        logger.LogInformation("[END] Handled {EventName} with CorrelationId: {CorrelationId}", nameof(IProductCreatedEvent), message.CorrelationId);    }
}