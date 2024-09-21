using System.Reflection;
using Catalog.Application;
using Catalog.Persistence;
using Catalog.Persistence.Extensions;
using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.MessageContracts.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMessageContracts(builder.Configuration)
    .AddCatalogApplication()
    .AddCatalogPersistence(builder.Configuration)
    .AddECommerceInfrastructure()
    .AddECommerceApi(Assembly.GetExecutingAssembly());

builder.Host.AddSerilog(builder.Configuration);

var app = builder.Build();

app.UseECommerceInfrastructure();

app.UseECommerceApi();

await app.MigrateDatabaseAsync();

app.Run();