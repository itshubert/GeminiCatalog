using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace GeminiCatalog.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(GeminiCatalogDbContext context) : base(context)
    {
    }

    public async Task<(int TotalRecords, IEnumerable<Product> Products)> GetProductsByCategoryAsync(
        Guid categoryId,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Products
            .Where(p => p.Categories.Any(c => c.Id == categoryId) && p.Active);
            
        var totalCount = await query.CountAsync();
        
        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalCount, products);
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }
}