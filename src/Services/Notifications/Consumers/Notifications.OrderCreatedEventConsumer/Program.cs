using ECommerce.MessageContracts.Extensions;
using MassTransit;
using Notifications.Consumers.Common;
using Notifications.OrderCreatedEventConsumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddNotificationsConsumerCommon(builder.Configuration)
    .AddMessageContracts(builder.Configuration,
        massTransitConfig =>
        {
            massTransitConfig.AddConsumer<OrderCreatedEventConsumer>();
        }, 
        (massTransitContext, rabbitMqConfig, queueName) =>
        {
            rabbitMqConfig.ReceiveEndpoint(queueName, endpointConfig =>
            {
                endpointConfig.ConfigureConsumer<OrderCreatedEventConsumer>(massTransitContext);
            });
        });

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();