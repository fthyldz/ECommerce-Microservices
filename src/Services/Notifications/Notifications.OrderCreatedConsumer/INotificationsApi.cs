using ECommerce.MessageBus.Events;
using ECommerce.MessageBus.Models;
using Refit;

namespace Notifications.OrderCreatedConsumer;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface INotificationsApi
{
    [Post("/notifications")]
    Task SendNotification([Body] NotificationDto request);
}