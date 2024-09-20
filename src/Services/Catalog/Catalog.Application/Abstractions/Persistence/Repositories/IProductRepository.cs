using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Domain.Entities;

namespace Catalog.Application.Abstractions.Persistence.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default);
    
    Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken = default);
}