using GeminiCatalog.Application.Common.Models.Products;

namespace GeminiCatalog.Application.Common.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryModel>> GetInventoryByProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
}