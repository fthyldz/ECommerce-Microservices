using Ordering.Application.Abstractions.Persistence.Repositories;
using Ordering.Domain.Entities;
using Ordering.Persistence.Common;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Repositories;

public class OrderRepository(OrderingDbContext context) : Repository<Order>(context), IOrderRepository
{
}