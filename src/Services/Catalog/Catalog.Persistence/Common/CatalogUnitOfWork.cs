using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Persistence.Contexts;
using ECommerce.Persistence.Common;

namespace Catalog.Persistence.Common;

public class CatalogUnitOfWork(CatalogDbContext context, IProductRepository productRepository, IStockRepository stockRepository) : UnitOfWork<CatalogDbContext>(context), ICatalogUnitOfWork
{
    public IProductRepository Products { get; } = productRepository;
    public IStockRepository Stocks { get; } = stockRepository;
}