using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Abstractions.Persistence.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default);
}