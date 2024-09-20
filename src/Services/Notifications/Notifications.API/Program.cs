using Carter;
using Notifications.Business;
using Notifications.Data;
using Notifications.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDataServices(builder.Configuration)
    .AddBusinessServices()
    .AddCarter();

var app = builder.Build();

app.MapCarter();

await app.MigrateDatabaseAsync();

app.Run();