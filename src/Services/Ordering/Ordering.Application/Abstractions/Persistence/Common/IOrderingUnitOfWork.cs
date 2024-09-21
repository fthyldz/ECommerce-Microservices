using Ordering.Application.Abstractions.Persistence.Repositories;
using ECommerce.Application.Abstractions.Persistence.Common;

namespace Ordering.Application.Abstractions.Persistence.Common;

public interface IOrderingUnitOfWork : IUnitOfWork
{
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
}