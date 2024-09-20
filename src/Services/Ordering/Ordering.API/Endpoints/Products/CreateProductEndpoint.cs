using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Products.Commands.CreateProduct;
using Ordering.Application.Products.Dtos;

namespace Ordering.API.Endpoints.Products;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async ([FromBody] ProductDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            await mediator.Send(new CreateProductCommand(request), cancellationToken);
            
            return Results.Ok();
        });
    }
}