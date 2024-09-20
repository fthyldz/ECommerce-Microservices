using Carter;
using Ordering.Application;
using Ordering.Persistence;
using Ordering.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrderingApplication()
    .AddOrderingPersistence(builder.Configuration)
    .AddCarter();

var app = builder.Build();

app.MapCarter();

await app.MigrateDatabaseAsync();

app.Run();