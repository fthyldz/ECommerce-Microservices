namespace Ordering.Application.Products.Dtos;

public record CreateProductDto(Guid ProductId, string Name, decimal Price, int Quantity, Guid CorrelationId);