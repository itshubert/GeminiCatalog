using ErrorOr;
using GeminiCatalog.Domain.Common.Models;

namespace GeminiCatalog.Domain.Products;

public sealed class Product : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public Price Price { get; private set; }
    public bool Active { get; private set; } = false;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Product(Guid id, string name, string description, Price price)
        : base(id)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Active = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static ErrorOr<Product> Create(
        string name,
        string description,
        Price price,
        Guid? id = null)
    {
        var priceResult = Price.Create(price.Value);
        if (priceResult.IsError)
        {
            return priceResult.Errors;
        }

        return new Product(
            id ?? Guid.NewGuid(),
            name,
            description,
            priceResult.Value);
    }
}
