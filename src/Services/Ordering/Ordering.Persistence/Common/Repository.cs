using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions.Persistence.Common;
using Ordering.Domain.Primitives;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Common;

public class Repository<TEntity>(OrderingDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression is null
            ? _dbSet
            : _dbSet.Where(expression);
    }

    public TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}