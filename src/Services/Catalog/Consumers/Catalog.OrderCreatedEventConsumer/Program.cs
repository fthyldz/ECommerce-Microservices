using Catalog.Consumers.Common;
using Catalog.OrderCreatedEventConsumer;
using ECommerce.MessageContracts.Extensions;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddCatalogConsumerCommon(builder.Configuration)
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