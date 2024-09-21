using System.Reflection;
using ECommerce.API;
using ECommerce.Infrastructure;
using Ordering.Application;
using Ordering.Persistence;
using Ordering.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrderingApplication()
    .AddOrderingPersistence(builder.Configuration)
    .AddECommerceInfrastructure()
    .AddECommerceApi(Assembly.GetExecutingAssembly());

builder.Host.AddSerilog(builder.Configuration);

var app = builder.Build();

app.UseECommerceInfrastructure();

app.UseECommerceApi();

await app.MigrateDatabaseAsync();

app.Run();