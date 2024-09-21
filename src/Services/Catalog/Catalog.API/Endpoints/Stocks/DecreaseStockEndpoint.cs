using Carter;
using Catalog.Application.Stocks.Commands.DecreaseStock;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Stocks;

public class DecreaseStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("stocks/decrease", async ([FromBody] DecreaseStockRequestDto request, IMediator mediator, HttpContext context, ILogger<DecreaseStockEndpoint> logger, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);

            var result = await mediator.Send(new DecreaseStockCommand(request), cancellationToken);
            
            logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, result);

            return Results.Ok(result.IsSuccess);
        });
    }
}