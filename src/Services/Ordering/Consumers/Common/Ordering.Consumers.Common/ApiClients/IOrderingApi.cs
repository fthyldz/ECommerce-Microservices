using Ordering.Consumers.Common.Dtos;
using Refit;

namespace Ordering.Consumers.Common.ApiClients;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface IOrderingApi
{
    [Post("/products")]
    Task<bool> CreateProductAsync([Header("X-Correlation-ID")] string correlationId, [Body] CreateProductDto request);
    
    [Put("/products")]
    Task<bool> UpdateStock([Header("X-Correlation-ID")] string correlationId, [Body] UpdateStockDto request);
}