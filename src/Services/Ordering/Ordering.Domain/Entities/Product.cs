using ECommerce.Domain.Primitives;

namespace Ordering.Domain.Entities;

public class Product : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    
    private Product()
    {
    }
    
    public static Product Create(Guid id, Guid productId, string name, decimal price, int quantity)
    {
        var product = new Product
        {
            Id = id,
            ProductId = productId,
            Name = name,
            Price = price,
            Quantity = quantity
        };

        return product;
    }
    
    public void UpdateStock(int quantity)
    {
        Quantity = quantity;
    }
}