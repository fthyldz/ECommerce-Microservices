using System.Reflection;
using Catalog.Application.Behaviors;
using Catalog.Application.Products;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.RegisterProductMappings();
        
        services.AddMassTransit(factory =>
        {
            factory.SetKebabCaseEndpointNameFormatter();
            
            factory.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://ecommerce-mq", config =>
                {
                    config.Username("guest");
                    config.Password("guest");
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}