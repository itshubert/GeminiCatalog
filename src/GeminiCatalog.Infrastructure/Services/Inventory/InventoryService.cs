using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Application.Common.Models.Products;

namespace GeminiCatalog.Infrastructure.Services.Inventory;

public sealed class InventoryService(InventoryServiceClient client) : IInventoryService
{
    public async Task<IEnumerable<InventoryModel>> GetInventoryByProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
    {

        var inventories = await client.GetInventoryByProductsAsync(productIds, cancellationToken: cancellationToken);

        return inventories.Select(i => new InventoryModel(
            i.ProductId,
            i.QuantityAvailable,
            i.QuantityReserved,
            i.LastRestockDate,
            i.MinimumStockLevel,
            i.UpdatedAt));
    }
}