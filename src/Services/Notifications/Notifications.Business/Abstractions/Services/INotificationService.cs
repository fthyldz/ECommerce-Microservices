using Notifications.Business.Dtos.Notifications;

namespace Notifications.Business.Abstractions.Services;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<NotificationDto> AddAsync(NotificationDto notificationDto, CancellationToken cancellationToken = default);
    
    Task<NotificationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}