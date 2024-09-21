using ECommerce.Domain.Primitives;

namespace Ordering.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    
    public Guid ProductId { get; private set; }

    public virtual Product Product { get; private set; } = default!;
    
    public int Quantity { get; private set; }
    
    public decimal Price { get; private set; }
    
    private OrderItem()
    {
    }
    
    public static OrderItem Create(Guid id, Guid orderId, Guid productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegative(price);
        
        var orderItem = new OrderItem
        {
            Id = id,
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Price = price
        };

        return orderItem;
    }
}