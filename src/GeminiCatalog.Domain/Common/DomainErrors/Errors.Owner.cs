using ErrorOr;

namespace BreadcrumbPostgres.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Owner
    {
        public static Error InvalidOwnerId => Error.Validation(
            code: "Owner.InvalidId",
            description: "The provided owner ID is invalid or does not exist.");
    }
}
