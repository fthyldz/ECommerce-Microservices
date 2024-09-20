using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Data.Entities;

namespace Notifications.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title).IsRequired();
        
        builder.Property(x => x.Content).IsRequired();
        
        builder.Property(x => x.CreatedAt).IsRequired();
        
        builder.Property(x => x.To).IsRequired();
        
        builder.Property(x => x.NotificationType).IsRequired();
    }
}