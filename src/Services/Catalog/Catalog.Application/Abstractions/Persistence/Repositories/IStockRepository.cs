using ECommerce.Application.Abstractions.Persistence.Common;
using Catalog.Domain.Entities;

namespace Catalog.Application.Abstractions.Persistence.Repositories;

public interface IStockRepository : IRepository<Stock>
{
    Task<Stock?> GetStockByProductId(Guid productId, CancellationToken cancellationToken);
}