namespace GeminiCatalog.Contracts;

public sealed record PagedResponse<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize);