using System.Linq.Expressions;
using ECommerce.Application.Abstractions.Persistence.Common;
using ECommerce.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Common;

public class Repository<TContext, TEntity>(TContext context) : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        return await (expression is null
            ? _dbSet.ToListAsync(cancellationToken)
            : _dbSet.Where(expression).ToListAsync(cancellationToken));
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.SingleOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
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