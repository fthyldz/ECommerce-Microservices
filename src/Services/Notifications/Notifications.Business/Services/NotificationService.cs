using System.Diagnostics;
using ECommerce.Application.Abstractions.Api.Common;
using Mapster;
using Microsoft.Extensions.Logging;
using Notifications.Business.Services.Abstractions;
using Notifications.Business.Dtos.Notifications;
using Notifications.Data.Entities;
using Notifications.Data.Enums;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Business.Services;

public class NotificationService(INotificationsUnitOfWork unitOfWork, ILogger<NotificationService> logger, ICorrelationIdService correlationIdService) : INotificationService
{
    public async Task<IEnumerable<NotificationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("[START] Handle Request=GetAllAsync");
        var timer = new Stopwatch();
        timer.Start();
        var notifications = await unitOfWork.Notifications.FindAllAsync(null, cancellationToken);

        var result = notifications.Adapt<IEnumerable<NotificationDto>>(TypeAdapterConfig<Notification, NotificationDto>
            .NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] The request GetAllAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        logger.LogInformation("[END] Handled GetAllAsync with {Response}", typeof(NotificationDto).Name);
        return result;
    }

    public async Task<NotificationDto> AddAsync(CreateNotificationDto notificationDto, CancellationToken cancellationToken = default)
    {
        var correlationId = correlationIdService.GetCorrelationId();
        logger.LogInformation("[START] Handle Request=AddAsync - RequestData={RequestData} - CorrelationId={CorrelationId}", notificationDto, correlationId);
        var timer = new Stopwatch();
        timer.Start();
        
        var notification = notificationDto.Adapt<Notification>(TypeAdapterConfig<CreateNotificationDto, Notification>
            .NewConfig()
            .ConstructUsing(src => Notification.Create(Guid.NewGuid(), src.Title, src.Content, src.To,
                (NotificationType)Enum.Parse(typeof(NotificationType), src.NotificationType))).Config);
        
        await unitOfWork.Notifications.AddAsync(notification, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = notification.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] The request AddAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        logger.LogInformation("[END] Handled AddAsync with {Response}", typeof(NotificationDto).Name);
        
        return result;
    }
    
    public async Task<NotificationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("[START] Handle Request=GetByIdAsync - RequestData={RequestData}", id);
        var timer = new Stopwatch();
        timer.Start();

        var notification = await unitOfWork.Notifications.FindAsync(x => x.Id == id, cancellationToken);
        
        var result = notification?.Adapt<NotificationDto>(TypeAdapterConfig<Notification, NotificationDto>.NewConfig()
            .Map(dest => dest.NotificationType, src => src.NotificationType.ToString()).Config);
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] The request GetByIdAsync took {TimeTaken} seconds.", timeTaken.Seconds);
        
        logger.LogInformation("[END] Handled GetByIdAsync with {Response}", typeof(NotificationDto).Name);

        return result;
    }
}