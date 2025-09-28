using GeminiCatalog.DataSeeder.Data;
using GeminiCatalog.Domain.Categories;
using GeminiCatalog.Domain.Common.Models;
using GeminiCatalog.Domain.Products;
using GeminiCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeminiCatalog.DataSeeder.Services;

public class DatabaseSeederService
{
    private readonly GeminiCatalogDbContext _context;
    private readonly ILogger<DatabaseSeederService> _logger;

    public DatabaseSeederService(GeminiCatalogDbContext context, ILogger<DatabaseSeederService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Starting database seeding...");

            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await _context.Categories.AnyAsync() || await _context.Products.AnyAsync())
            {
                _logger.LogInformation("Database already contains data. Skipping seeding.");
                return;
            }

            // Seed categories first
            await SeedCategoriesAsync();
            await _context.SaveChangesAsync();

            // Seed products and associate with categories
            await SeedProductsAsync();
            await _context.SaveChangesAsync();

            _logger.LogInformation("Database seeding completed successfully.");
            _logger.LogInformation($"Created {SeedData.Categories.Count} categories and {SeedData.Products.Count} products.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedCategoriesAsync()
    {
        _logger.LogInformation("Seeding categories...");

        await _context.Categories.AddRangeAsync(SeedData.Categories);
        
        _logger.LogInformation($"Added {SeedData.Categories.Count} categories to the database.");
    }

    private async Task SeedProductsAsync()
    {
        _logger.LogInformation("Seeding products...");

        var categoriesInDb = await _context.Categories.ToListAsync();
        var categoryLookup = categoriesInDb.ToDictionary(c => c.Name, c => c);

        foreach (var productSeed in SeedData.Products)
        {
            var priceResult = Price.Create(productSeed.Price);
            if (priceResult.IsError)
            {
                _logger.LogWarning($"Failed to create price for product {productSeed.Name}: {string.Join(", ", priceResult.Errors)}");
                continue;
            }

            var productResult = Product.Create(
                productSeed.Name,
                productSeed.Description,
                priceResult.Value);

            if (productResult.IsError)
            {
                _logger.LogWarning($"Failed to create product {productSeed.Name}: {string.Join(", ", productResult.Errors)}");
                continue;
            }

            var product = productResult.Value;

            // Add categories to the product
            foreach (var categoryName in productSeed.CategoryNames)
            {
                if (categoryLookup.TryGetValue(categoryName, out var category))
                {
                    product.AddCategory(category);
                }
                else
                {
                    _logger.LogWarning($"Category '{categoryName}' not found for product '{productSeed.Name}'");
                }
            }

            _context.Products.Add(product);
        }

        _logger.LogInformation($"Added {SeedData.Products.Count} products to the database.");
    }

    public async Task ClearDataAsync()
    {
        _logger.LogInformation("Clearing existing data...");

        // Remove products first (due to foreign key constraints)
        _context.Products.RemoveRange(_context.Products);
        await _context.SaveChangesAsync();

        // Remove categories
        _context.Categories.RemoveRange(_context.Categories);
        await _context.SaveChangesAsync();

        _logger.LogInformation("All existing data has been cleared.");
    }
}