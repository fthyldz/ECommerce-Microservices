using Carter;
using MediatR;
using Ordering.Application.Products.Queries.GetAllProducts;

namespace Ordering.API.Endpoints.Products;

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
            
            return Results.Ok(result);
        });
    }
}