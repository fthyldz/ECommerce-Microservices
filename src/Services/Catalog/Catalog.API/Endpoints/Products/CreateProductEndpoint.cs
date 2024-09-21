using Carter;
using Catalog.Application.Products.Commands.CreateProduct;
using MediatR;

namespace Catalog.API.Endpoints.Products;


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductDto request, IMediator mediator, ILogger<CreateProductEndpoint> logger, HttpContext context, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);

            var result = await mediator.Send(new CreateProductCommand(request), cancellationToken);

            logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, result);

            return Results.Created($"/products/{result.Id}", result);
        });
    }
}