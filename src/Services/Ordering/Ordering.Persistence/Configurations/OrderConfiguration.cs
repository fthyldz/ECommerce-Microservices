using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired();

        builder.Property(o => o.CustomerName).IsRequired();

        builder.Property(o => o.OrderDate).IsRequired();
        
        builder.Property(o => o.TotalPrice).HasPrecision(18, 2).IsRequired();
        
        builder.Property(o => o.Address).IsRequired();
    }
}