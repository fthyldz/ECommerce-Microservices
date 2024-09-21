using Catalog.Application.Abstractions.Persistence.Common;
using Catalog.Application.Abstractions.Persistence.Repositories;
using Catalog.Persistence.Common;
using Catalog.Persistence.Contexts;
using Catalog.Persistence.Repositories;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Persistence;

public static class ServiceRegistrar
{
    public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDbConnection");
        
        services.AddECommercePersistence<CatalogDbContext>();

        services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetService<ISaveChangesInterceptor>()!);
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();

        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        
        return services;
    }
}