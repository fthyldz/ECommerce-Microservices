using Catalog.Domain.Entities;
using Catalog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catalog.Persistence.Extensions;

public static class DatabaseInitializer
{
    public static async Task MigrateDatabaseAsync(this IHost app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        await context.Database.MigrateAsync();
        
        await context.SeedData();
    }
    
    private static async Task SeedData(this CatalogDbContext context)
    {
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                Product.Create(Guid.Parse("b10f0916-f5b3-405e-bf62-62d821ad3854"), "Product 1", "Description 1", 100, Stock.Create(Guid.Parse("595fea92-8861-4092-86a1-f3bea1b24377"), Guid.Parse("b10f0916-f5b3-405e-bf62-62d821ad3854"), 10)),
                Product.Create(Guid.Parse("5f74ebec-6baf-42a9-9f31-bf75c24c7e3a"), "Product 2", "Description 2", 200, Stock.Create(Guid.Parse("3ce3b64a-24ac-49ad-bdc7-c3f0d42c1fd9"), Guid.Parse("5f74ebec-6baf-42a9-9f31-bf75c24c7e3a"), 20)),
                Product.Create(Guid.Parse("68f91c54-4336-4b1b-a99e-c91ebbbbd61d"), "Product 3", "Description 3", 300, Stock.Create(Guid.Parse("6203fa87-7e04-4e3f-bb87-1c248d9b6773"), Guid.Parse("68f91c54-4336-4b1b-a99e-c91ebbbbd61d"), 30))
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}