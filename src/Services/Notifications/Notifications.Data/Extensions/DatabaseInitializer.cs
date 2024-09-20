using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Data.Contexts;
using Notifications.Data.Entities;
using Notifications.Data.Enums;

namespace Notifications.Data.Extensions;

public static class DatabaseInitializer
{
    public static async Task MigrateDatabaseAsync(this IHost app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        await context.Database.MigrateAsync();
        
        await context.SeedData();
    }
    
    private static async Task SeedData(this NotificationsDbContext context)
    {
        if (!context.Notifications.Any())
        {
            var notifications = new List<Notification>
            {
                Notification.Create(Guid.Parse("b10f0916-f5b3-405e-bf62-62d821ad3854"), "Notification 1", "Description 1", "Fatih YILDIZ", NotificationType.Email),
                Notification.Create(Guid.Parse("5f74ebec-6baf-42a9-9f31-bf75c24c7e3a"), "Notification 2", "Description 2", "Fatih YILDIZ", NotificationType.Sms),
                Notification.Create(Guid.Parse("68f91c54-4336-4b1b-a99e-c91ebbbbd61d"), "Notification 3", "Description 3", "Fatih YILDIZ", NotificationType.Email)
            };

            await context.Notifications.AddRangeAsync(notifications);
            await context.SaveChangesAsync();
        }
    }
}