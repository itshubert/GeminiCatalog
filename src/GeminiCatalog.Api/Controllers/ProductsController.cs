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

}

