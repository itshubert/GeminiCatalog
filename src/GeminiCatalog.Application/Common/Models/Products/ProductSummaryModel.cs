namespace GeminiCatalog.Application.Common.Models.Products;

/// <summary>
/// Simplified Product model for list operations
/// </summary>
public record ProductSummaryModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool Active
);