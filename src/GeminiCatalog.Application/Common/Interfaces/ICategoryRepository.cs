using GeminiCatalog.Domain.Categories;

namespace GeminiCatalog.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken cancellationToken);
}