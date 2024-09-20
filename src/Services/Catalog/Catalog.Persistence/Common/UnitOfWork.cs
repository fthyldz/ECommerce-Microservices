using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Persistence.Contexts;

namespace Catalog.Persistence.Common;

public class UnitOfWork(CatalogDbContext context, IProductRepository productRepository, IStockRepository stockRepository) : IUnitOfWork
{
    public IProductRepository Products { get; } = productRepository;
    public IStockRepository Stocks { get; } = stockRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}