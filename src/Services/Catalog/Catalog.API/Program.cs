using Carter;
using Catalog.Application;
using Catalog.Persistence;
using Catalog.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogApplication()
    .AddCatalogPersistence(builder.Configuration)
    .AddCarter();

var app = builder.Build();

app.MapCarter();

await app.MigrateDatabaseAsync();

app.Run();