using GeminiCatalog.Domain.Common.Models;
using GeminiCatalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeminiCatalog.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        // Configure Price as a value object using conversion
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnName("Price")
            .HasColumnType("decimal(18,2)")
            .HasConversion(
                price => price.Value,
                value => Price.Create(value).Value);

        builder.Property(p => p.Active)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

        // Many-to-many relationship with Categories is configured in CategoryConfiguration
    }
}