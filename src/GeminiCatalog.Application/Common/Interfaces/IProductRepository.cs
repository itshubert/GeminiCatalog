namespace GeminiCatalog.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Domain.Products.Product>> GetProductsByCategoryAsync(string categoryName);
    Task<Domain.Products.Product?> GetProductByIdAsync(Guid productId);
}