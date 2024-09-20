using Mapster;
using Notifications.Business.Abstractions.Services;
using Notifications.Business.Dtos.Notifications;
using Notifications.Data.Entities;
using Notifications.Data.Enums;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Business.Services;

public class NotificationService(INotificationRepository notificationRepository) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    
    public async Task<IEnumerable<NotificationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notifications = await _notificationRepository.FindAllAsync(null, cancellationToken);
        
        return notifications.Adapt<IEnumerable<NotificationDto>>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
    }

    public async Task<NotificationDto> AddAsync(NotificationDto notificationDto, CancellationToken cancellationToken = default)
    {
        var notification = notificationDto.Adapt<Notification>(TypeAdapterConfig<NotificationDto, Notification>
            .NewConfig()
            .ConstructUsing(src => Notification.Create(Guid.NewGuid(), src.Title, src.Content, src.To,
                (NotificationType)Enum.Parse(typeof(NotificationType), src.NotificationType))).Config);
        
        await _notificationRepository.AddAsync(notification, cancellationToken);
        
        return notification.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
    }
    
    public async Task<NotificationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var notification = await _notificationRepository.FindAsync(x => x.Id == id, cancellationToken);
        
        return notification?.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
    }
}