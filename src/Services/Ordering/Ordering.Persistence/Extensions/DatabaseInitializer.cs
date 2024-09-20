using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Persistence.Contexts;

namespace Ordering.Persistence.Extensions;

public static class DatabaseInitializer
{
    public static async Task MigrateDatabaseAsync(this IHost app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();

        await context.Database.MigrateAsync();
    }
}