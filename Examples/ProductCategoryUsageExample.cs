using ErrorOr;
using GeminiCatalog.Domain.Categories;
using GeminiCatalog.Domain.Common.Models;
using GeminiCatalog.Domain.Products;

namespace GeminiCatalog.Examples;

/// <summary>
/// Example usage of the Product-Category many-to-many relationship
/// Note: All timestamps (CreatedAt, UpdatedAt) use DateTimeOffset.UtcNow for consistent UTC handling
/// </summary>
public class ProductCategoryUsageExample
{
    public static ErrorOr<Product> CreateProductWithCategories()
    {
        // Create categories
        var electronics = Category.Create("Electronics", "Electronic devices and accessories");
        var laptops = Category.Create("Laptops", "Portable computers");
        var gaming = Category.Create("Gaming", "Gaming related products");

        // Create a price
        var priceResult = Price.Create(1299.99m);
        if (priceResult.IsError)
        {
            return priceResult.Errors;
        }

        // Create a product
        var productResult = Product.Create(
            "Gaming Laptop",
            "High-performance gaming laptop with RTX graphics",
            priceResult.Value);

        if (productResult.IsError)
        {
            return productResult.Errors;
        }

        var product = productResult.Value;

        // Add the product to multiple categories
        product.AddCategory(electronics);
        product.AddCategory(laptops);
        product.AddCategory(gaming);

        // Also add the product to categories from the category side
        electronics.AddProduct(product);
        laptops.AddProduct(product);
        gaming.AddProduct(product);

        return product;
    }

    public static void ManageCategoryRelationships(Product product, Category category)
    {
        // Add category to product
        product.AddCategory(category);
        
        // Add product to category
        category.AddProduct(product);

        // Remove category from product
        product.RemoveCategory(category);
        
        // Remove product from category  
        category.RemoveProduct(product);

        // Clear all categories from product
        product.ClearCategories();
        
        // Clear all products from category
        category.ClearProducts();
    }
}