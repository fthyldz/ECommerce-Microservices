namespace ECommerce.MessageBus.Models;

public record NotificationDto(string Title, string Content, string To, string NotificationType);