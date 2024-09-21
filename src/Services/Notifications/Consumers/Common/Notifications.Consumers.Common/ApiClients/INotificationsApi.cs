using Notifications.Consumers.Common.Dtos;
using Refit;

namespace Notifications.Consumers.Common.ApiClients;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface INotificationsApi
{
    [Post("/notifications")]
    Task SendNotification([Header("X-Correlation-ID")] string correlationId, [Body] NotificationRequestDto request);
}