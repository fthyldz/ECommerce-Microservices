namespace Ordering.Application.Orders.Dtos;

public record OrderItemDto(Guid ProductId, int Quantity, decimal Price);