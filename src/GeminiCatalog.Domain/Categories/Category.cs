using GeminiCatalog.Domain.Common.Models;
using GeminiCatalog.Domain.Products;

namespace GeminiCatalog.Domain.Categories;

public sealed class Category : AggregateRoot<Guid>
{
    private readonly List<Product> _products = new();

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    private Category(Guid id, string name, string description)
        : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    // Parameterless constructor for EF Core
#pragma warning disable CS8618
    private Category() : base() { }
#pragma warning restore CS8618

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

    public void AddProduct(Product product)
    {
        if (!_products.Contains(product))
        {
            _products.Add(product);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }

    public void RemoveProduct(Product product)
    {
        if (_products.Remove(product))
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }

    public void ClearProducts()
    {
        _products.Clear();
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}