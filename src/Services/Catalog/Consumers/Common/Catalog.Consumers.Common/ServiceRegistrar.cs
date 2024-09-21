using Catalog.Consumers.Common.ApiClients;
using ECommerce.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Catalog.Consumers.Common;

public static class ServiceRegistrar
{
    public static IServiceCollection AddCatalogConsumerCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddECommerceConsumer(configuration);
        
        services
            .AddRefitClient<ICatalogApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("CatalogApi:Url")!));

        return services;
    }
}