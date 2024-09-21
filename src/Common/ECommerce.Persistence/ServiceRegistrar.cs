using ECommerce.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class ServiceRegistrar
{
    public static IServiceCollection AddECommercePersistence<TContext>(this IServiceCollection services) 
        where TContext : DbContext
    {
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        return services;
    }
}