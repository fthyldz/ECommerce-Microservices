using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Domain.Entities;
using ECommerce.Persistence.Common;
using Catalog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class StockRepository(CatalogDbContext context) : Repository<CatalogDbContext, Stock>(context), IStockRepository
{
    public async Task<Stock?> GetStockByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await _dbSet.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }
}