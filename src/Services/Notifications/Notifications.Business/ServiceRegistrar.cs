using Microsoft.Extensions.DependencyInjection;
using Notifications.Business.Abstractions.Services;
using Notifications.Business.Services;

namespace Notifications.Business;

public static class ServiceRegistrar
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}