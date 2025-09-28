namespace GeminiCatalog.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository
{
    protected readonly GeminiCatalogDbContext _context;

    protected BaseRepository(GeminiCatalogDbContext context)
    {
        _context = context;
    }
}