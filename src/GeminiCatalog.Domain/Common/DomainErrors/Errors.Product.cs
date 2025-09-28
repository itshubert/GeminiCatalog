using ErrorOr;

namespace GeminiCatalog.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "The specified product was not found.");

        public static Error InvalidProductId => Error.Validation(
            code: "Product.InvalidId",
            description: "The provided product ID is invalid.");
    }
}