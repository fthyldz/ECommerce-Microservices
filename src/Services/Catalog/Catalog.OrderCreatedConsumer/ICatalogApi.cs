using ECommerce.MessageBus.Dtos;
using Refit;

namespace Catalog.OrderCreatedConsumer;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface ICatalogApi
{
    [Put("/stocks/decrease")]
    Task<bool> DecreaseStock([Body] DecreaseStockRequestDto request);
}