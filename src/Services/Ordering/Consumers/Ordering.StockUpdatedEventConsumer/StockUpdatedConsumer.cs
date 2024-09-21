using ECommerce.MessageContracts.Events;
using Mapster;
using MassTransit;
using Ordering.Consumers.Common.ApiClients;
using Ordering.Consumers.Common.Dtos;
using Polly;

namespace Ordering.StockUpdatedEventConsumer;

public class StockUpdatedEventConsumer(ILogger<StockUpdatedEventConsumer> logger, IOrderingApi orderingApi, AsyncPolicy asyncPolicy) : IConsumer<IStockUpdatedEvent>
{
    public async Task Consume(ConsumeContext<IStockUpdatedEvent> context)
    {
        var message = context.Message;
        
        logger.LogInformation("Handling ProductCreatedEvent with CorrelationId: {CorrelationId}, EventId: {EventId}, and Message: {@Message}", message.CorrelationId, message.EventId, message);        
        var request = message.Adapt<UpdateStockDto>();
        
        var result = await asyncPolicy.ExecuteAsync(() => orderingApi.UpdateStock(message.CorrelationId.ToString(), request));
        
        if (result)
            logger.LogInformation("Stock update successful for ProductId: {ProductId}, CorrelationId: {CorrelationId}, EventId: {EventId}", message.ProductId, message.CorrelationId, message.EventId);
        else
            logger.LogError("Stock update failed for ProductId: {ProductId}, CorrelationId: {CorrelationId}, EventId: {EventId}", message.ProductId, message.CorrelationId, message.EventId);
        
        logger.LogInformation("[END] Handled {EventName} with CorrelationId: {CorrelationId}", nameof(IProductCreatedEvent), message.CorrelationId);
    }
}