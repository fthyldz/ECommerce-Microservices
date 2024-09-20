using ECommerce.MessageBus.Events;
using Refit;

namespace Ordering.ProductCreatedConsumer;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface IOrderingApi
{
    [Post("/products")]
    Task<bool> CreateProduct([Body] IProductCreatedEvent request);
}