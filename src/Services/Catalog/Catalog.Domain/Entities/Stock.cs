using ECommerce.Domain.Primitives;

namespace Catalog.Domain.Entities;

public class Stock : BaseEntity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime LastUpdated { get; private set; }
    
    private Stock()
    {
    }
    
    public static Stock Create(Guid id, Guid productId, int quantity)
    {
        var stock = new Stock
        {
            Id = id,
            ProductId = productId,
            Quantity = quantity,
            LastUpdated = DateTime.UtcNow
        };

        return stock;
    }
    
    public void DecreaseStock(int quantity)
    {
        Quantity -= quantity;
        LastUpdated = DateTime.UtcNow;
    }
}