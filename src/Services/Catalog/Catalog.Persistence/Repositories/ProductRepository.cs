using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Domain.Entities;
using Catalog.Persistence.Common;
using Catalog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class ProductRepository(CatalogDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await Find().Include(x => x.Stock).ToListAsync(cancellationToken);
    }
}