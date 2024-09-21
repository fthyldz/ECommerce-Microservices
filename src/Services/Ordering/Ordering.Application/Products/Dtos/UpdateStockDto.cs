namespace Ordering.Application.Products.Dtos;

public record UpdateStockDto(Guid ProductId, int Quantity);