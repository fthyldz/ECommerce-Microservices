using ECommerce.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Consumers.Common.ApiClients;
using Refit;

namespace Notifications.Consumers.Common;

public static class ServiceRegistrar
{
    public static IServiceCollection AddNotificationsConsumerCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddECommerceConsumer(configuration);
        
        services
            .AddRefitClient<INotificationsApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("NotificationsApi:Url")!));

        return services;
    }
}