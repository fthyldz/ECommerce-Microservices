using System.Reflection;
using ECommerce.Application.Cqrs.Behaviors;
using ECommerce.Application.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddECommerceApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddPollyResilience();
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}