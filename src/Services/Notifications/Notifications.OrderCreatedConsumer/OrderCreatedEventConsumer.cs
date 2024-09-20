using System.Text.Json;
using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using MassTransit;

namespace Notifications.OrderCreatedConsumer;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var notificationsApi = context.GetServiceOrCreateInstance<INotificationsApi>();

        var message = context.Message;

        var notificationSmsDto = new NotificationDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Sms"
        );
        
        var notificationEmailDto = new NotificationDto(
            "Order Created",
            $"Order created with {message.OrderId} OrderId for customer {message.CustomerName}",
            message.CustomerName,
            "Email"
        );
        
        await notificationsApi.SendNotification(notificationSmsDto);
        await notificationsApi.SendNotification(notificationEmailDto);
    }
}