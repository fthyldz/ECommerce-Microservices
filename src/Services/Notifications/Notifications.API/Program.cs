using System.Reflection;
using ECommerce.API;
using ECommerce.Infrastructure;
using Notifications.Business;
using Notifications.Data;
using Notifications.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDataServices(builder.Configuration)
    .AddBusinessServices()
    .AddECommerceInfrastructure()
    .AddECommerceApi(Assembly.GetExecutingAssembly());

builder.Host.AddSerilog(builder.Configuration);

var app = builder.Build();

app.UseECommerceInfrastructure();

app.UseECommerceApi();

await app.MigrateDatabaseAsync();

app.Run();