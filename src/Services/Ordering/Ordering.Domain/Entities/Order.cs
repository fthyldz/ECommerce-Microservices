using Ordering.Domain.Events.DomainEvents;
using Ordering.Domain.Primitives;

namespace Ordering.Domain.Entities;

public class Order : Aggregate
{
    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    public string CustomerName { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public DateTime OrderDate { get; private set; }
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }
    
    public static Order Create(Guid id, string customerName, string address)
    {
        var order = new Order
        {
            Id = id,
            CustomerName = customerName,
            OrderDate = DateTime.UtcNow,
            Address = address
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }
    
    public void AddOrderItem(Guid orderItemId, Guid orderId, Guid productId, int quantity, decimal price)
    {
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);
        _orderItems.Add(orderItem);
    }
}