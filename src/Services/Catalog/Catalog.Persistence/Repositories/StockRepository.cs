using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Domain.Entities;
using Catalog.Persistence.Common;
using Catalog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class StockRepository(CatalogDbContext context) : Repository<Stock>(context), IStockRepository
{
    public async Task<Stock?> GetStockByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await context.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }
}