namespace ECommerce.Application.Abstractions.Persistence.Common;

public interface IUnitOfWork
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}