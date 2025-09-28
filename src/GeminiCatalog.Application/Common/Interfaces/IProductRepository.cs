using GeminiCatalog.Domain.Products;

namespace GeminiCatalog.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<(int TotalRecords, IEnumerable<Product> Products)> GetProductsByCategoryAsync(
        Guid categoryId,
        int pageNumber,
        int pageSize);
    Task<Product?> GetProductByIdAsync(Guid productId);
}