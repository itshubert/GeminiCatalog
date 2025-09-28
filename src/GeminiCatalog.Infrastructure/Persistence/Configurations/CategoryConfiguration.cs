using GeminiCatalog.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeminiCatalog.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

        builder.Property(c => c.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

        // Configure many-to-many relationship with Products
        builder.HasMany(c => c.Products)
            .WithMany(p => p.Categories)
            .UsingEntity("ProductCategories",
                j => j.HasOne(typeof(Domain.Products.Product)).WithMany().HasForeignKey("ProductId"),
                j => j.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId"),
                j =>
                {
                    j.HasKey("ProductId", "CategoryId");
                    j.ToTable("ProductCategories");
                });
    }
}