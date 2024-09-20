using System.Diagnostics;
using Mapster;
using Microsoft.Extensions.Logging;
using Notifications.Business.Abstractions.Services;
using Notifications.Business.Dtos.Notifications;
using Notifications.Data.Entities;
using Notifications.Data.Enums;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Business.Services;

public class NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly ILogger<NotificationService> _logger = logger;
    
    public async Task<IEnumerable<NotificationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[START] Handle Request=GetAllAsync");
        var timer = new Stopwatch();
        timer.Start();
        var notifications = await _notificationRepository.FindAllAsync(null, cancellationToken);

        var result = notifications.Adapt<IEnumerable<NotificationDto>>(TypeAdapterConfig<Notification, NotificationDto>
            .NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            _logger.LogWarning("[PERFORMANCE] The request GetAllAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        _logger.LogInformation("[END] Handled GetAllAsync with {Response}", typeof(NotificationDto).Name);
        return result;
    }

    public async Task<NotificationDto> AddAsync(CreateNotificationDto notificationDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[START] Handle Request=AddAsync - RequestData={RequestData} - CorrelationId={CorrelationId}", notificationDto, notificationDto.CorrelationId);
        var timer = new Stopwatch();
        timer.Start();
        
        var notification = notificationDto.Adapt<Notification>(TypeAdapterConfig<CreateNotificationDto, Notification>
            .NewConfig()
            .ConstructUsing(src => Notification.Create(Guid.NewGuid(), src.Title, src.Content, src.To,
                (NotificationType)Enum.Parse(typeof(NotificationType), src.NotificationType))).Config);
        
        await _notificationRepository.AddAsync(notification, cancellationToken);
        
        var result = notification.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            _logger.LogWarning("[PERFORMANCE] The request AddAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        _logger.LogInformation("[END] Handled AddAsync with {Response}", typeof(NotificationDto).Name);
        
        return result;
    }
    
    public async Task<NotificationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[START] Handle Request=GetByIdAsync - RequestData={RequestData}", id);
        var timer = new Stopwatch();
        timer.Start();

        var notification = await _notificationRepository.FindAsync(x => x.Id == id, cancellationToken);
        
        var result = notification?.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            _logger.LogWarning("[PERFORMANCE] The request GetByIdAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        _logger.LogInformation("[END] Handled GetByIdAsync with {Response}", typeof(NotificationDto).Name);

        return result;
    }
}