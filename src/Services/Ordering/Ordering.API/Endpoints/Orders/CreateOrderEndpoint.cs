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
        app.MapPost("/orders", async ([FromBody] OrderDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = new CreateOrderCommand(request);
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(result.IsSuccess);
        });
    }
}