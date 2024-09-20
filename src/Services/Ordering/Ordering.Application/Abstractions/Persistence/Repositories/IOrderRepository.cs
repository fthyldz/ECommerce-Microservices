using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Abstractions.Persistence.Repositories;

public interface IOrderRepository : IRepository<Order>
{
}