using Catalog.Domain.Events;
using ECommerce.Domain.Primitives;

namespace Catalog.Domain.Entities;

public class Product : Aggregate
{
    public string Name { get; private set; } = default!;
    public string Description { get; private  set; } = default!;
    public decimal Price { get; private  set; }

    public Guid StockId { get; private set; }
    public virtual Stock Stock { get; private set; } = default!;
    
    private Product()
    {
    }
    
    public static Product Create(Guid id, string name, string description, decimal price, Stock stock)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            StockId = stock.Id,
            Stock = stock
        };
        
        product.AddDomainEvent(new ProductCreatedDomainEvent(product));

        return product;
    }
    
    public void DecreaseStock(int quantity)
    {
        Stock.DecreaseStock(quantity);
        
        AddDomainEvent(new StockUpdatedDomainEvent(Id, Stock.Quantity));
    }
}