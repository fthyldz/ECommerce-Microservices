using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Data.Contexts;
using Notifications.Data.Primitives;
using Notifications.Data.Repositories.Abstractions.Common;

namespace Notifications.Data.Repositories.Common;

public class Repository<TEntity>(NotificationsDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        return expression is null
            ? await _dbSet.ToListAsync(cancellationToken)
            : await _dbSet.Where(expression).ToListAsync(cancellationToken);
    }
    
    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}