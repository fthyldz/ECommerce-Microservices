using ECommerce.Application.Abstractions.Api.Common;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ECommerce.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection AddECommerceInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICorrelationIdService, CorrelationIdService>();
        
        return services;
    }
    
    public static void AddSerilog(this ConfigureHostBuilder builder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithCorrelationIdHeader("X-Correlation-ID")
            .CreateBootstrapLogger();
        
        builder.UseSerilog();
    }
    
    public static WebApplication UseECommerceInfrastructure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        
        return app;
    }
}