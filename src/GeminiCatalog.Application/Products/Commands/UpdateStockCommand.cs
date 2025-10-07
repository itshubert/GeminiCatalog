using ErrorOr;
using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Application.Common.Models.Products;
using MediatR;

namespace GeminiCatalog.Application.Products.Commands;

public sealed record UpdateStockCommand(Guid ProductId, int NewStock) : IRequest<ErrorOr<StockStatus>>;

public sealed class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, ErrorOr<StockStatus>>
{
    private readonly IProductRepository _repository;

    public UpdateStockCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<StockStatus>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Error.Failure(code: "Product.NotFound", description: "Product not found.");
        }

        product.UpdateStock(request.NewStock);
        await _repository.UpdateProductAsync(product, cancellationToken);

        return product.StockStatus switch
        {
            Domain.Products.StockStatus.InStock => StockStatus.InStock,
            _ => StockStatus.OutOfStock
        };
    }
}