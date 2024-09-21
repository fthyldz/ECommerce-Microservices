using ECommerce.MessageContracts.Events;
using MassTransit;
using Notifications.Consumers.Common.ApiClients;
using Notifications.Consumers.Common.Dtos;
using Polly;

namespace Notifications.OrderCreatedEventConsumer;

public class OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, INotificationsApi notificationsApi, AsyncPolicy asyncPolicy) : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var message = context.Message;

        logger.LogInformation("Handling ProductCreatedEvent with CorrelationId: {CorrelationId}, EventId: {EventId}, and Message: {@Message}", message.CorrelationId, message.EventId, message);        
        
        var notificationSmsDto = new NotificationRequestDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Sms"
        );
        
        var notificationEmailDto = new NotificationRequestDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Email"
        );

        await asyncPolicy.ExecuteAsync(() => notificationsApi.SendNotification(message.CorrelationId.ToString(), notificationSmsDto));
        await asyncPolicy.ExecuteAsync(() => notificationsApi.SendNotification(message.CorrelationId.ToString(), notificationEmailDto));

        logger.LogInformation("[END] Handled {EventName} with CorrelationId: {CorrelationId}", nameof(IProductCreatedEvent), message.CorrelationId);
    }
}