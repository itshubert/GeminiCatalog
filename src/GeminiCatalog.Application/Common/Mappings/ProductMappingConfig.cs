using GeminiCatalog.Application.Common.Models.Products;
using GeminiCatalog.Domain.Products;
using Mapster;

namespace GeminiCatalog.Application.Common.Mappings;

public sealed class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductSummaryModel>()
            .Map(dest => dest.Price, src => src.Price.Value)
            .Map(dest => dest, src => src);
    }
}