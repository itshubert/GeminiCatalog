using GeminiCatalog.Application.Common.Interfaces;
using GeminiCatalog.Infrastructure.Persistence;
using GeminiCatalog.Infrastructure.Persistence.Interceptors;
using GeminiCatalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeminiCatalog.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GeminiCatalogDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("GeminiCatalogDbContext");
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
