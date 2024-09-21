using Catalog.Consumers.Common.Dtos;
using Refit;

namespace Catalog.Consumers.Common.ApiClients;

[Headers("Content-Type: application/json;charset=utf-8", "Accept: application/json")]
public interface ICatalogApi
{
    [Put("/stocks/decrease")]
    Task<bool> DecreaseStock([Header("X-Correlation-ID")] string correlationId, [Body] DecreaseStockRequestDto request);
}