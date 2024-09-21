using ECommerce.MessageContracts.Extensions;
using MassTransit;
using Ordering.Consumers.Common;
using Ordering.ProductCreatedEventConsumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddOrderingConsumerCommon(builder.Configuration)
    .AddMessageContracts(builder.Configuration,
    massTransitConfig =>
{
    massTransitConfig.AddConsumer<ProductCreatedEventConsumer>();
}, 
    (massTransitContext, rabbitMqConfig, queueName) =>
{
    rabbitMqConfig.ReceiveEndpoint(queueName, endpointConfig =>
    {
        endpointConfig.ConfigureConsumer<ProductCreatedEventConsumer>(massTransitContext);
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();