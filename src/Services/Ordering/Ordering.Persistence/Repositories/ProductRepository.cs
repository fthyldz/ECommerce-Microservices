using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Domain.Entities;
using Ordering.Persistence.Common;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Repositories;

public class ProductRepository(OrderingDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return await Find().ToListAsync(cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<Product>> GetProductByIdsAsync(Guid[] productIds, CancellationToken cancellationToken = default)
    {
        return await Find(x => productIds.Contains(x.ProductId)).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Find(x => x.ProductId == id).FirstOrDefaultAsync(cancellationToken);
    }
}