using System.Linq.Expressions;
using Catalog.Domain.Primitives;

namespace Catalog.Application.Abstractions.Persistence.Common;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null);
    TEntity Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}