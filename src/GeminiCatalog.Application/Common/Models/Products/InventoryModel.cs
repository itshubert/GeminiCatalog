namespace GeminiCatalog.Application.Common.Models.Products;

public sealed record InventoryModel(
    Guid ProductId,
    int QuantityAvailable,
    int QuantityReserved,
    DateTimeOffset LastRestockDate,
    int MinimumStockLevel,
    DateTimeOffset UpdatedAt);