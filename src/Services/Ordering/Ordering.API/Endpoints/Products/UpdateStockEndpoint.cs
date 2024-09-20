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
        app.MapPut("/products", async ([FromBody] StockUpdateDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new UpdateStockCommand(request), cancellationToken);
            
            return Results.Ok(response.IsSuccess);
        });
    }
}