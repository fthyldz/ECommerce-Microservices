using Carter;
using Notifications.Business.Abstractions.Services;

namespace Notifications.API.Endpoints.Notifications;

public class GetByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/notifications/{id:guid}",
            async (Guid id, INotificationService notificationService, CancellationToken cancellationToken = default) =>
            {
                var notification = await notificationService.GetByIdAsync(id, cancellationToken);
                
                return Results.Ok(notification);
            });
    }
}