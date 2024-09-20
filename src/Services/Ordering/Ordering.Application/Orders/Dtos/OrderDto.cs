namespace Ordering.Application.Orders.Dtos;

public record OrderDto(string CustomerName, string Address, List<OrderItemDto> OrderItems);