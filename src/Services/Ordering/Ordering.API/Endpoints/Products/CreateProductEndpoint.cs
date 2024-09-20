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
        app.MapPost("/products", async ([FromBody] CreateProductDto request, IMediator mediator, ILogger<CreateProductEndpoint> logger, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[START] ProductId: {ProductId}, CorrelationId: {CorrelationId}, ClassType: {ClassType}", request.ProductId, request.CorrelationId, nameof(CreateProductEndpoint));
            var result = await mediator.Send(new CreateProductCommand(request), cancellationToken);
            logger.LogInformation("[END] ProductId: {ProductId}, CorrelationId: {CorrelationId}, ClassType: {ClassType}", request.ProductId, request.CorrelationId, nameof(CreateProductEndpoint));
            return Results.Ok(result.IsSuccess);
        });
    }
}