using Catalog.Application.Abstractions.Persistence.Repositories;
using ECommerce.Application.Abstractions.Persistence.Common;

namespace Catalog.Application.Abstractions.Persistence.Common;

public interface ICatalogUnitOfWork : IUnitOfWork
{
    IProductRepository Products { get; }
    IStockRepository Stocks { get; }
}