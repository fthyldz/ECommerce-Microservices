using ECommerce.Application.Abstractions.Persistence.Common;

namespace Notifications.Data.Repositories.Abstractions;

public interface INotificationsUnitOfWork : IUnitOfWork
{
    INotificationRepository Notifications { get; }
}