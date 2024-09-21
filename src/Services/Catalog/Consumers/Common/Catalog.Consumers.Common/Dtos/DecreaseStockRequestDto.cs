namespace Catalog.Consumers.Common.Dtos;

public record DecreaseStockRequestDto(Guid ProductId, int Quantity);