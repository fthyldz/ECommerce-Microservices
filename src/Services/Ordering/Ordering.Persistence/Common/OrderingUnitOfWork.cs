using ECommerce.Persistence.Common;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Common;

public class OrderingUnitOfWork(OrderingDbContext context, IOrderRepository orderRepository, IProductRepository productRepository) : UnitOfWork<OrderingDbContext>(context), IOrderingUnitOfWork
{
    public IOrderRepository Orders => orderRepository;
    public IProductRepository Products => productRepository;
}