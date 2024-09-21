namespace Ordering.Consumers.Common.Dtos;

public class UpdateStockDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}