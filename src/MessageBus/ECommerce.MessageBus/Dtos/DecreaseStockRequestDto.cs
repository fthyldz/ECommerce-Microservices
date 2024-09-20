namespace ECommerce.MessageBus.Dtos;

public record DecreaseStockRequestDto(Guid ProductId, int Quantity);