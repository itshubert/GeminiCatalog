using ErrorOr;
using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Application.Common.Models.Common;
using GeminiCatalog.Application.Common.Models.Products;
using GeminiCatalog.Domain.Common.DomainErrors;
using MapsterMapper;
using MediatR;

namespace GeminiCatalog.Application.Products.Queries;

public sealed record GetProductsByCategoryQuery(Guid CategoryId, int PageNumber, int PageSize)
    : IRequest<ErrorOr<PagedResultModel<ProductSummaryModel>>>;

public sealed class GetProductsByCategoryQueryHandler
    : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<PagedResultModel<ProductSummaryModel>>>
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryQueryHandler(
        IProductRepository repository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<PagedResultModel<ProductSummaryModel>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryRepository.CategoryExistsAsync(request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            return Errors.Category.InvalidCategoryId;
        }

        var (totalRecords, products) = await _repository.GetProductsByCategoryAsync(request.CategoryId, request.PageNumber, request.PageSize);

        var productSummaries = _mapper.Map<IEnumerable<ProductSummaryModel>>(products);

        return new PagedResultModel<ProductSummaryModel>(
            Items: productSummaries,
            TotalCount: totalRecords,
            PageNumber: request.PageNumber,
            PageSize: request.PageSize
        );
    }
}