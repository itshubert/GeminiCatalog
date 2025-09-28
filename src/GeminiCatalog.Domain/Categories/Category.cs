using GeminiCatalog.Domain.Common.Models;

namespace GeminiCatalog.Domain.Categories;

public sealed class Category : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Category(Guid id, string name, string description)
        : base(id)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Category Create(
        string name,
        string description,
        Guid? id = null)
    {
        return new Category(
            id ?? Guid.NewGuid(),
            name,
            description);
    }
}