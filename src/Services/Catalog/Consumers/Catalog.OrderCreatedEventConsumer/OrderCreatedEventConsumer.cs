using Catalog.Consumers.Common.ApiClients;
using Catalog.Consumers.Common.Dtos;
using ECommerce.MessageContracts.Events;
using Mapster;
using MassTransit;
using Polly;

namespace Catalog.OrderCreatedEventConsumer;

public class OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, ICatalogApi catalogApi, AsyncPolicy asyncPolicy) : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var message = context.Message;

        logger.LogInformation("Handling ProductCreatedEvent with CorrelationId: {CorrelationId}, EventId: {EventId}, and Message: {@Message}", message.CorrelationId, message.EventId, message);        
        
        foreach (var orderItem in message.OrderItems)
        {
            var request = orderItem.Adapt<DecreaseStockRequestDto>();
            var result = await asyncPolicy.ExecuteAsync(() => catalogApi.DecreaseStock(message.CorrelationId.ToString(), request));
            
            if (result)
                logger.LogInformation("Stock decreased successfully for ProductId: {ProductId}, Quantity: {Quantity}, CorrelationId: {CorrelationId}", orderItem.ProductId, orderItem.Quantity, message.CorrelationId);
            else
                logger.LogError("Failed to decrease stock for ProductId: {ProductId}, Quantity: {Quantity}, CorrelationId: {CorrelationId}", orderItem.ProductId, orderItem.Quantity, message.CorrelationId);
        }
        
        logger.LogInformation("[END] Handled {EventName} with CorrelationId: {CorrelationId}", nameof(IProductCreatedEvent), message.CorrelationId);
    }
}