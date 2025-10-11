using GeminiCatalog.Application.Categories.Queries;
using GeminiCatalog.Contracts.Categories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeminiCatalog.Api.Controllers;

[Route("[controller]")]
public sealed class CategoriesController : ApiController
{
    public CategoriesController(
        ISender sender,
        IMapper mapper) : base(sender, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetAllCategoriesQuery();
        var response = await Mediator.Send(query);

        return Ok(response.Select(c => new CategoryResponse(c.Id, c.Name, c.Description)));
    }
}