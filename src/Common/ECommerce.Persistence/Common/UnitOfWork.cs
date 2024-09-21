using ECommerce.Application.Abstractions.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Common;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork
    where TContext : DbContext
{
    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}