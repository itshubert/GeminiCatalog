using GeminiCatalog.Application.Products.Commands;
using GeminiCatalog.Application.Products.Queries;
using GeminiCatalog.Contracts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeminiCatalog.Api.Controllers;

[Route("[controller]")]
public sealed class ProductsController : ApiController
{
    public ProductsController(
        ISender sender,
        IMapper mapper) : base(sender, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(
        [FromQuery] Guid categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetProductsByCategoryQuery(categoryId, page, pageSize);
        var response = await Mediator.Send(query);

        return response.Match(
            result => Ok(
                new PagedResponse<ProductSummaryResponse>(
                Mapper.Map<IEnumerable<ProductSummaryResponse>>(result.Items),
                result.TotalCount,
                result.PageNumber,
                result.PageSize
                )
            ),
            errors => Problem(errors));
    }

    [HttpGet("{productId:guid}/summary")]
    public async Task<IActionResult> GetProductSummary([FromRoute] Guid productId)
    {
        var query = new GetProductSummaryQuery(productId);
        var response = await Mediator.Send(query);

        return response.Match(
            result => result is not null
                ? Ok(Mapper.Map<ProductSummaryResponse>(result))
                : NotFound(),
            Problem);
    }

    [HttpPost("refresh-stocks")]
    public async Task<IActionResult> RefreshAllStocks([FromBody] IEnumerable<Guid> productIds, CancellationToken cancellationToken)
    {
        var command = new RefreshAllStocksCommand(productIds, cancellationToken);
        var response = await Mediator.Send(command);

        return response.Match(
            result => Ok(),
            Problem);
    }

}

