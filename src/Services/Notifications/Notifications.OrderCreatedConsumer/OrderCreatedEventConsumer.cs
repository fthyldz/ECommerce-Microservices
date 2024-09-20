using System.Text.Json;
using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;
using Polly;

namespace Notifications.OrderCreatedConsumer;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var logger = context.GetServiceOrCreateInstance<ILogger<OrderCreatedEventConsumer>>();
        
        logger.LogInformation("[START] Handle Request={Request} - RequestData={RequestData}",
            nameof(IOrderCreatedEvent), context.Message);
        
        var message = context.Message;

        logger.LogInformation("{Event} yakalandı. CorrelationId: {CorrelationId}", nameof(IOrderCreatedEvent), message.CorrelationId);

        var notificationSmsDto = new NotificationDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Sms",
            message.CorrelationId
        );
        
        var notificationEmailDto = new NotificationDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Email",
            message.CorrelationId
        );
        
        var notificationsApi = context.GetServiceOrCreateInstance<INotificationsApi>();
        var asyncPolicy = context.GetServiceOrCreateInstance<AsyncPolicy>();

        await asyncPolicy.ExecuteAsync(() => notificationsApi.SendNotification(notificationSmsDto));
        await asyncPolicy.ExecuteAsync(() => notificationsApi.SendNotification(notificationEmailDto));

        logger.LogInformation("[END] Handled {Request}", nameof(IOrderCreatedEvent));
    }
}