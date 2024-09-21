using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Dtos;

namespace Ordering.API.Endpoints.Orders;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async ([FromBody] OrderDto request, IMediator mediator, HttpContext context, ILogger<CreateOrderEndpoint> logger, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);

            var result = await mediator.Send(new CreateOrderCommand(request), cancellationToken);

            logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, result);

            return Results.Ok(result.IsSuccess);
        });
    }
}