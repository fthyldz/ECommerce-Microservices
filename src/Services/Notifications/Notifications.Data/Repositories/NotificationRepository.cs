using Notifications.Data.Contexts;
using Notifications.Data.Entities;
using ECommerce.Persistence.Common;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Data.Repositories;

public class NotificationRepository(NotificationsDbContext context) : Repository<NotificationsDbContext, Notification>(context), INotificationRepository
{
}