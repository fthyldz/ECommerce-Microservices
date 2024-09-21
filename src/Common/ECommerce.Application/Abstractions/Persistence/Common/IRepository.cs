using System.Linq.Expressions;
using ECommerce.Domain.Primitives;

namespace ECommerce.Application.Abstractions.Persistence.Common;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}