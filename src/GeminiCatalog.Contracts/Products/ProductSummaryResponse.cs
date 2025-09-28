namespace GeminiCatalog.Contracts;

public sealed record ProductSummaryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool Active
);