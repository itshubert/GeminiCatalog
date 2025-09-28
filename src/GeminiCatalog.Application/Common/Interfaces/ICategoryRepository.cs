namespace GeminiCatalog.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken cancellationToken);
}