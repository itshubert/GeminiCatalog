using ErrorOr;
using GeminiCatalog.Application.Common.Interfaces;
using MediatR;

namespace GeminiCatalog.Application.Products.Commands;

public sealed record RefreshAllStocksCommand(IEnumerable<Guid> ProductIds, CancellationToken CancellationToken) : IRequest<ErrorOr<Unit>>;

public sealed class RefreshAllStocksCommandHandler : IRequestHandler<RefreshAllStocksCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _repository;
    private readonly IInventoryService _inventoryService;

    public RefreshAllStocksCommandHandler(IProductRepository repository, IInventoryService inventoryService)
    {
        _repository = repository;
        _inventoryService = inventoryService;
    }

    public async Task<ErrorOr<Unit>> Handle(RefreshAllStocksCommand request, CancellationToken cancellationToken)
    {
        var stocks = await _inventoryService.GetInventoryByProductsAsync(request.ProductIds, cancellationToken);
        var productsToUpdate = await _repository.GetByProductIds(stocks.Select(s => s.ProductId), cancellationToken);

        foreach (var product in productsToUpdate)
        {
            var stockInfo = stocks.FirstOrDefault(s => s.ProductId == product.Id);
            if (stockInfo != null)
            {
                product.UpdateStock(stockInfo.QuantityAvailable);
                await _repository.UpdateProductAsync(product, cancellationToken);
            }
        }

        return Unit.Value;
    }
}