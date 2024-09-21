using System.Reflection;
using Catalog.Application.Products;
using ECommerce.Application;
using ECommerce.Application.Cqrs.Behaviors;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddECommerceApplication(Assembly.GetExecutingAssembly());
        
        services.RegisterProductMappings();
        
        return services;
    }
}