using System.Reflection;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviors;

namespace Ordering.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddOrderingApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
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