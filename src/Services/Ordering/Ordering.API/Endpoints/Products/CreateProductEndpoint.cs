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
        app.MapPost("/products", async ([FromBody] CreateProductDto request, IMediator mediator, HttpContext context, ILogger<CreateProductEndpoint> logger, CancellationToken cancellationToken = default) =>
        {
            logger.LogInformation("[REQUEST] CorrelationId: {CorrelationId}, Request: {@Request}", context.TraceIdentifier, request);
            
            var result = await mediator.Send(new CreateProductCommand(request), cancellationToken);
            
            logger.LogInformation("[RESPONSE] CorrelationId: {CorrelationId}, Response: {@Response}", context.TraceIdentifier, result);
            return Results.Ok(result.IsSuccess);
        });
    }
}