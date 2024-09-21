using ECommerce.Consumer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ECommerce.Consumer;

public static class ServiceRegistrar
{
    public static IServiceCollection AddECommerceConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithCorrelationIdHeader("X-Correlation-ID")
            .CreateBootstrapLogger();
        
        services.AddSerilog();
        
        services.AddPollyResilience();
        
        return services;
    }
}