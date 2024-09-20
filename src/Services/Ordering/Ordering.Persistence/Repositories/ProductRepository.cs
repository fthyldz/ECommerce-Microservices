using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Domain.Entities;
using Ordering.Persistence.Common;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Repositories;

public class ProductRepository(OrderingDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await Find().ToListAsync(cancellationToken);
    }
}