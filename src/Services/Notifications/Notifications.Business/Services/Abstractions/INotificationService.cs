using Notifications.Business.Dtos.Notifications;

namespace Notifications.Business.Services.Abstractions;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<NotificationDto> AddAsync(CreateNotificationDto notificationDto, CancellationToken cancellationToken = default);
    
    Task<NotificationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}