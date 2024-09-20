namespace Notifications.Business.Dtos.Notifications;

public record NotificationDto(Guid Id, string Title, string Content, string To, string NotificationType);