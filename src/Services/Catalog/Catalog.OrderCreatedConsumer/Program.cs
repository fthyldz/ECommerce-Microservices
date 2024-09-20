using Catalog.OrderCreatedConsumer;
using ECommerce.MessageBus;
using MassTransit;
using Refit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(factory =>
{
    factory.SetKebabCaseEndpointNameFormatter();

    factory.AddConsumer<OrderCreatedEventConsumer>();
    
    factory.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://ecommerce-mq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ReceiveEndpoint(RabbitMqConstants.CatalogServiceQueue, e =>
        {
            e.Consumer<OrderCreatedEventConsumer>(context);
        });
    });
});
    
builder.Services
    .AddRefitClient<ICatalogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://catalog.api:8080"));

var host = builder.Build();
host.Run();