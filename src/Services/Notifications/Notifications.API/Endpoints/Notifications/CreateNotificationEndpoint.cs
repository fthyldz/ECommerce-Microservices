using Carter;
using Microsoft.AspNetCore.Mvc;
using Notifications.Business.Services.Abstractions;
using Notifications.Business.Dtos.Notifications;

namespace Notifications.API.Endpoints.Notifications;

public class CreateNotificationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/notifications",
            async ([FromBody] CreateNotificationDto request, INotificationService notificationService, ILogger<CreateNotificationEndpoint> logger, HttpContext context, CancellationToken cancellationToken = default) =>
            {
                logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);

                var response = await notificationService.AddAsync(request, cancellationToken);
                
                logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, response);
                
                return Results.Created($"/notifications/{response.Id}", response);
            });
    }
}