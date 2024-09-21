using System.Reflection;
using Carter;
using ECommerce.API.Middlewares;
using ECommerce.Api.Middlewares.ExceptionHandlers;
using ECommerce.API.Middlewares.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.API;

public static class ServiceRegister
{
    public static IServiceCollection AddECommerceApi(this IServiceCollection services, Assembly assembly)
    {
        services.AddCarter(new DependencyContextAssemblyCatalog([assembly]));
        
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
    
    public static WebApplication UseECommerceApi(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.MapCarter();
        
        app.UseExceptionHandler();
        
        return app;
    }
}