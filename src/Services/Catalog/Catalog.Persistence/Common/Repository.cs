using System.Linq.Expressions;
using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Domain.Primitives;
using Catalog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Common;

public class Repository<TEntity>(CatalogDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

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