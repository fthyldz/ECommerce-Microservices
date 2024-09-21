using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Products.Commands.UpdateStock;
using Ordering.Application.Products.Dtos;

namespace Ordering.API.Endpoints.Products;

public class UpdateStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async ([FromBody] UpdateStockDto request, IMediator mediator, HttpContext context, ILogger<UpdateStockEndpoint> logger, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);
            
            var result = await mediator.Send(new UpdateStockCommand(request), cancellationToken);

            logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, result);
            
            return Results.Ok(result.IsSuccess);
        });
    }
}