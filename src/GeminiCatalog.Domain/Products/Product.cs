using ErrorOr;
using GeminiCatalog.Domain.Categories;
using GeminiCatalog.Domain.Common.Models;

namespace GeminiCatalog.Domain.Products;

public sealed class Product : AggregateRoot<Guid>
{
    private readonly List<Category> _categories = new();

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public Price Price { get; private set; }
    public bool Active { get; private set; } = false;
    public int AvailableStock { get; private set; } = 0;
    public StockStatus StockStatus { get; private set; } = StockStatus.OutOfStock;
    public DateTimeOffset? LastStockUpdate { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private Product(Guid id, string name, string description, Price price)
        : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Active = true;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    // Parameterless constructor for EF Core
#pragma warning disable CS8618
    private Product() : base() { }
#pragma warning restore CS8618

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

    public void AddCategory(Category category)
    {
        if (!_categories.Contains(category))
        {
            _categories.Add(category);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }

    public void RemoveCategory(Category category)
    {
        if (_categories.Remove(category))
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }

    public void ClearCategories()
    {
        _categories.Clear();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    // TODO: Need to consume InventoryLevelChanged event to update stock

    public void UpdateStock(int newStock)
    {
        AvailableStock = newStock;
        StockStatus = newStock > 0 ? StockStatus.InStock : StockStatus.OutOfStock;
        LastStockUpdate = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void SetStockStatus(StockStatus stockStatus)
    {
        StockStatus = stockStatus;
        LastStockUpdate = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
