namespace ECommerce.MessageBus;

public static class RabbitMqConstants
{
    public const string OrderingServiceQueue = "ordering.service";
    public const string OrderingServiceStockUpdatedQueue = "ordering.service.stockupdated";
    public const string NotificationsServiceQueue = "notifications.service";
    public const string CatalogServiceQueue = "catalog.service";
}