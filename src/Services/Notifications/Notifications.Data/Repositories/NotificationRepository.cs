using Notifications.Data.Contexts;
using Notifications.Data.Entities;
using Notifications.Data.Repositories.Abstractions;
using Notifications.Data.Repositories.Common;

namespace Notifications.Data.Repositories;

public class NotificationRepository(NotificationsDbContext context) : Repository<Notification>(context), INotificationRepository
{
}