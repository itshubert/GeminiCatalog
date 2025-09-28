namespace GeminiCatalog.Application.Common.Models.Products;

/// <summary>
/// Application-level DTO for Product data used within CQRS handlers
/// </summary>
public record ProductModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Sku,
    int StockQuantity,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid? CategoryId,
    string? CategoryName
);