using Carter;
using Notifications.Business.Services.Abstractions;

namespace Notifications.API.Endpoints.Notifications;

public class GetAllEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/notifications",
            async (INotificationService notificationService, CancellationToken cancellationToken = default) =>
            {
                var notifications = await notificationService.GetAllAsync(cancellationToken);
                
                return Results.Ok(notifications);
            });
    }
}