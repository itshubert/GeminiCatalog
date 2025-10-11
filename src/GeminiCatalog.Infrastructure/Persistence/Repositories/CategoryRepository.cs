using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace GeminiCatalog.Infrastructure.Persistence.Repositories;

public sealed class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(GeminiCatalogDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}