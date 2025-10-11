namespace GeminiCatalog.Application.Common.Models.Categories;

public sealed record CategoryModel(
    Guid Id,
    string Name,
    string Description);