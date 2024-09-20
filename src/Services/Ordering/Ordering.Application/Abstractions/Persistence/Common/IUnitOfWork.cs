using Ordering.Application.Abstractions.Persistence.Repositories;

namespace Ordering.Application.Abstractions.Persistence.Common;

public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    
    IProductRepository Products { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}