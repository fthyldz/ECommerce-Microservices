using ECommerce.MessageBus;
using MassTransit;
using Notifications.OrderCreatedConsumer;
using Notifications.OrderCreatedConsumer.Extensions;
using Refit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddPollyResilience();

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
        
        cfg.ReceiveEndpoint(RabbitMqConstants.NotificationsServiceQueue, e =>
        {
            e.Consumer<OrderCreatedEventConsumer>(context);
            
            e.UseMessageRetry(config =>
            {
                config.Interval(5, TimeSpan.FromSeconds(10));
            });
        });
    });
});
    
builder.Services
    .AddRefitClient<INotificationsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://notifications.api:8080"));


var host = builder.Build();
host.Run();