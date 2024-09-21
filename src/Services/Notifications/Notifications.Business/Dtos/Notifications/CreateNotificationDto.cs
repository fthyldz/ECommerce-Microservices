namespace Notifications.Business.Dtos.Notifications;

public record CreateNotificationDto(Guid Id, string Title, string Content, string To, string NotificationType);