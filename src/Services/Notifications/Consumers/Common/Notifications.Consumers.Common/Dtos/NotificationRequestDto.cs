namespace Notifications.Consumers.Common.Dtos;

public record NotificationRequestDto(string Title, string Content, string To, string NotificationType);