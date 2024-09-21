using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Domain.Entities;
using ECommerce.Persistence.Common;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Repositories;

public class OrderRepository(OrderingDbContext context) : Repository<OrderingDbContext, Order>(context), IOrderRepository
{
}