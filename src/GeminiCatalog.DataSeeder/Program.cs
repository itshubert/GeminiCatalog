using GeminiCatalog.Application;
using GeminiCatalog.DataSeeder.Services;
using GeminiCatalog.Infrastructure.Persistence;
using GeminiCatalog.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// Add configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Add Application services (includes MediatR)
builder.Services.AddApplication();

// Add Entity Framework
builder.Services.AddScoped<PublishDomainEventsInterceptor>();
builder.Services.AddDbContext<GeminiCatalogDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("GeminiCatalogDbContext");
    options.UseNpgsql(connectionString);
});

// Add seeder service
builder.Services.AddScoped<DatabaseSeederService>();

var host = builder.Build();

// Get services and run seeding
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    var seeder = services.GetRequiredService<DatabaseSeederService>();

    logger.LogInformation("Starting GeminiCatalog Database Seeder...");

    // Check for command line arguments
    var cmdArgs = Environment.GetCommandLineArgs();
    if (cmdArgs.Length > 1 && cmdArgs[1].ToLower() == "--clear")
    {
        logger.LogInformation("Clear flag detected. Clearing existing data...");
        await seeder.ClearDataAsync();
        logger.LogInformation("Data cleared successfully.");
    }
    else
    {
        await seeder.SeedAsync();
    }

    logger.LogInformation("Database seeding operation completed successfully!");
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during the seeding process.");
    Environment.Exit(1);
}
