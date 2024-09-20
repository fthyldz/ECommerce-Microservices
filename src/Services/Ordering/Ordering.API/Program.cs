using Carter;
using Ordering.API.Middlewares.ExceptionHandlers;
using Ordering.Application;
using Ordering.Persistence;
using Ordering.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrderingApplication()
    .AddOrderingPersistence(builder.Configuration)
    .AddCarter();

builder.Services.AddExceptionHandlers();

var app = builder.Build();

app.MapCarter();

await app.MigrateDatabaseAsync();

app.Run();