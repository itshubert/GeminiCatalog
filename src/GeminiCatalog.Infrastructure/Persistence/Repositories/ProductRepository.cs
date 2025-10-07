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
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Where(p => p.Categories.Any(c => c.Id == categoryId) && p.Active);

        var totalCount = await query.CountAsync(cancellationToken);

        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalCount, products);
    }

    public async Task<IEnumerable<Product>> GetByProductIds(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}