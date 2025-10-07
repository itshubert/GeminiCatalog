using GeminiCatalog.Domain.Products;

namespace GeminiCatalog.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByProductIds(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
    Task<(int TotalRecords, IEnumerable<Product> Products)> GetProductsByCategoryAsync(
        Guid categoryId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
}