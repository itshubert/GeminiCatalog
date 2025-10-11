using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Application.Common.Models.Categories;
using MediatR;

namespace GeminiCatalog.Application.Categories.Queries;

public sealed record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryModel>>;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryModel>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories.Select(c => new CategoryModel(c.Id, c.Name, c.Description));
    }
}