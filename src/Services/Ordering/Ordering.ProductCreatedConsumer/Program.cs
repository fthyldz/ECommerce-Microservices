using ECommerce.MessageBus;
using MassTransit;
using Ordering.ProductCreatedConsumer;
using Ordering.ProductCreatedConsumer.Extensions;
using Refit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddPollyResilience();

builder.Services.AddMassTransit(factory =>
{
    factory.SetKebabCaseEndpointNameFormatter();

    factory.AddConsumer<ProductCreatedEventConsumer>();
    
    factory.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://ecommerce-mq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ReceiveEndpoint(RabbitMqConstants.OrderingServiceQueue, e =>
        {
            e.Consumer<ProductCreatedEventConsumer>(context);
            
            e.UseMessageRetry(config =>
            {
                config.Interval(5, TimeSpan.FromSeconds(10));
            });
        });
    });
});
    
builder.Services
    .AddRefitClient<IOrderingApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://ordering.api:8080"));

var host = builder.Build();
host.Run();
