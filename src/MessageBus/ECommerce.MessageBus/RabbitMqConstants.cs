namespace ECommerce.MessageBus;

public static class RabbitMqConstants
{
    public const string RabbitMqUri = "amqp://ecommerce-mq:5672";
    public const string Username = "guest";
    public const string Password = "guest";
    public const string OrderingServiceQueue = "ordering.service";
    public const string NotificationsServiceQueue = "notifications.service";
    public const string CatalogServiceQueue = "catalog.service";
}