using Carter;
using Microsoft.AspNetCore.Mvc;
using Notifications.Business.Abstractions.Services;
using Notifications.Business.Dtos.Notifications;

namespace Notifications.API.Endpoints.Notifications;

public class CreateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/notifications",
            async ([FromBody] NotificationDto notificationDto, INotificationService notificationService, CancellationToken cancellationToken = default) =>
            {
                var notification = await notificationService.AddAsync(notificationDto, cancellationToken);

                return Results.Created($"/notifications/{notification.Id}", notification);
            });
    }
}