using ECommerce.MessageBus.Events;
using Refit;

namespace Ordering.StockUpdatedConsumer;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface IOrderingApi
{
    [Put("/products")]
    Task<bool> UpdateStock([Body] IStockUpdatedEvent request);
}