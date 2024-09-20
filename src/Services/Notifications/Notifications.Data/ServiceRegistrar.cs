using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Data.Contexts;
using Notifications.Data.Repositories;
using Notifications.Data.Repositories.Abstractions;

namespace Notifications.Data;

public static class ServiceRegistrar
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NotificationsDbConnection");
        
        services.AddDbContext<NotificationsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<INotificationRepository, NotificationRepository>();
        
        return services;
    }
}