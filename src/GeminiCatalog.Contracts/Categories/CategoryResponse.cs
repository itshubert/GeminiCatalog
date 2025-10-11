namespace GeminiCatalog.Contracts.Categories;

public sealed record CategoryResponse(
    Guid Id,
    string Name,
    string Description
);