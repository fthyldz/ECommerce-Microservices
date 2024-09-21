namespace Ordering.Consumers.Common.Dtos;

public record CreateProductDto(Guid CorrelationId, Guid ProductId, string Name, decimal Price, int Quantity);