using ECommerce.MessageContracts.Configurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ECommerce.MessageContracts.Extensions;

public static class MessageContractsExtensions
{
    public static IServiceCollection AddMessageContracts(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? massTransitConfigurator = null, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator, string>? registrationAction = null)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        
        services.AddMassTransit(masstransitConfig =>
        {
            massTransitConfigurator?.Invoke(masstransitConfig);
            
            masstransitConfig.UsingRabbitMq((context, rabbitmqConfig) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                rabbitmqConfig.Host(options.HostName, options.Port, options.VirtualHost, hostConfig =>
                {
                    hostConfig.Username(options.UserName);
                    hostConfig.Password(options.Password);
                });
        
                rabbitmqConfig.UseMessageRetry(config =>
                {
                    config.Interval(5, TimeSpan.FromSeconds(10));
                });
                
                registrationAction?.Invoke(context, rabbitmqConfig, options.QueueName);
            });
        });
        
        return services;
    }
}