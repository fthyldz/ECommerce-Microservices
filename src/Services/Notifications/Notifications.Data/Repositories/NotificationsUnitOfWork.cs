using ECommerce.Persistence.Common;
using Notifications.Data.Contexts;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Data.Repositories;

public class NotificationsUnitOfWork(NotificationsDbContext context, INotificationRepository notificationRepository) : UnitOfWork<NotificationsDbContext>(context), INotificationsUnitOfWork
{
    public INotificationRepository Notifications { get; } = notificationRepository;
}