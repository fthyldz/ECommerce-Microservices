using ECommerce.Application.Abstractions.Persistence.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Abstractions.Persistence.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> GetProductByIdsAsync(Guid[] productIds, CancellationToken cancellationToken = default);
    
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);

}