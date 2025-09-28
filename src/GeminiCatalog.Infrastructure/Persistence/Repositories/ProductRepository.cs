using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace GeminiCatalog.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(GeminiCatalogDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
    {
        return await _context.Products
            .Where(p => p.Categories.Any(c => c.Name == categoryName))
            .ToListAsync();
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }
}