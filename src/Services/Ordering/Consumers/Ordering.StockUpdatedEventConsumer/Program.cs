using ECommerce.MessageContracts.Extensions;
using MassTransit;
using Ordering.Consumers.Common;
using Ordering.StockUpdatedEventConsumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddOrderingConsumerCommon(builder.Configuration)
    .AddMessageContracts(builder.Configuration,
        massTransitConfig =>
        {
            massTransitConfig.AddConsumer<StockUpdatedEventConsumer>();
        }, 
        (massTransitContext, rabbitMqConfig, queueName) =>
        {
            rabbitMqConfig.ReceiveEndpoint(queueName, endpointConfig =>
            {
                endpointConfig.ConfigureConsumer<StockUpdatedEventConsumer>(massTransitContext);
            });
        });

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();