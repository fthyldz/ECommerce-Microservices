using Carter;
using Catalog.Application.Stocks.Commands.DecreaseStock;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Stocks;

public record DecreaseStockRequest(Guid ProductId, int Quantity);

public class DecreaseStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("stocks/decrease", async ([FromBody] DecreaseStockRequest request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var command = request.Adapt<DecreaseStockCommand>();
            
            var result = await mediator.Send(command, cancellationToken);
            
            return Results.Ok(result);
        });
    }
}