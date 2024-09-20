using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Common;

public class UnitOfWork(OrderingDbContext context, IOrderRepository orderRepository, IProductRepository productRepository) : IUnitOfWork
{
    public IOrderRepository Orders => orderRepository;

    public IProductRepository Products => productRepository;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}