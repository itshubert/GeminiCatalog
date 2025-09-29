using ErrorOr;
using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Application.Common.Models.Products;
using GeminiCatalog.Domain.Common.DomainErrors;
using MapsterMapper;
using MediatR;

namespace GeminiCatalog.Application.Products.Queries;

public sealed record GetProductSummaryQuery(
    Guid ProductId) : IRequest<ErrorOr<ProductSummaryModel?>>;

public sealed class GetProductSummaryQueryHandler(
    IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<GetProductSummaryQuery, ErrorOr<ProductSummaryModel?>>
{
    public async Task<ErrorOr<ProductSummaryModel?>> Handle(GetProductSummaryQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null)
        {
            return Errors.Product.NotFound;
        }

        return mapper.Map<ProductSummaryModel>(product);
    }
}