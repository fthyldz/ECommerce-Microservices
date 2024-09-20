namespace Ordering.Application.Products.Dtos;

public record StockUpdateDto(Guid ProductId, int Quantity);