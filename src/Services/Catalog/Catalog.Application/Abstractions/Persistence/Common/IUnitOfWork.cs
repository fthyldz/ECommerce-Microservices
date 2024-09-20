using Catalog.Application.Abstractions.Persistence.Repositories;

namespace Catalog.Application.Abstractions.Persistence.Common;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IStockRepository Stocks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}