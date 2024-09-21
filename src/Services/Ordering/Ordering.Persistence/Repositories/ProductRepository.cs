using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Domain.Entities;
using ECommerce.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Repositories;

public class ProductRepository(OrderingDbContext context) : Repository<OrderingDbContext, Product>(context), IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductByIdsAsync(Guid[] productIds, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => productIds.Contains(x.ProductId)).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
    }
}