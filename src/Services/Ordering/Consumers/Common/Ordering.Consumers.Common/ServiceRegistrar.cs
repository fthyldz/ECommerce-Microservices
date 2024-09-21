using ECommerce.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Consumers.Common.ApiClients;
using Refit;

namespace Ordering.Consumers.Common;

public static class ServiceRegistrar
{
    public static IServiceCollection AddOrderingConsumerCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddECommerceConsumer(configuration);
        
        services
            .AddRefitClient<IOrderingApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("OrderingApi:Url")!));

        return services;
    }
}