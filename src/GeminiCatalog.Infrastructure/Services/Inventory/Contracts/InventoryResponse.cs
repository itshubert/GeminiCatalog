namespace GeminiCatalog.Infrastructure.Services.Inventory.Contracts;

public sealed record InventoryResponse(
    Guid ProductId,
    int QuantityAvailable,
    int QuantityReserved,
    DateTimeOffset LastRestockDate,
    int MinimumStockLevel,
    DateTimeOffset UpdatedAt);