# Entity Framework Migrations Commands

This file contains the common EF Core migration commands for the GeminiCatalog project.

## Prerequisites
- Ensure PostgreSQL is installed and running
- Update connection strings in appsettings.json and appsettings.Development.json
- Make sure the target database exists

## Migration Commands

### Create a new migration
```powershell
dotnet ef migrations add <MigrationName> --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Apply migrations to database
```powershell
dotnet ef database update --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Remove the last migration (if not applied to database yet)
```powershell
dotnet ef migrations remove --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### List all migrations
```powershell
dotnet ef migrations list --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Generate SQL script from migrations
```powershell
dotnet ef migrations script --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Generate SQL script for specific migration range
```powershell
dotnet ef migrations script <FromMigration> <ToMigration> --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Update to specific migration
```powershell
dotnet ef database update <MigrationName> --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

### Clear all migrations and start fresh
```powershell
# Remove all migration files
Remove-Item ".\src\GeminiCatalog.Infrastructure\Migrations\*" -Force

# Create new initial migration
dotnet ef migrations add InitialCreate --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

## Current Migrations
1. `20250928024016_InitialCreate` - Fresh initial migration with all features:
   - Products, Categories, and ProductCategories tables
   - Many-to-many relationship with proper foreign keys
   - DateTimeOffset for UTC timestamps with PostgreSQL defaults
   - Price value object as decimal(18,2) column

## Database Schema Created
- **Products Table**: 
  - Stores product information with Price as decimal(18,2)
  - Uses DateTimeOffset for CreatedAt/UpdatedAt with UTC database defaults
  - Many-to-many relationship with Categories
  
- **Categories Table**: 
  - Stores category information
  - Uses DateTimeOffset for CreatedAt/UpdatedAt with UTC database defaults
  - Many-to-many relationship with Products
  
- **ProductCategories Table**: 
  - Junction table for many-to-many relationship between Products and Categories
  - Composite primary key (ProductId, CategoryId)
  - Cascade delete on both foreign keys

## UTC Timestamp Handling
- **Domain Models**: Use `DateTimeOffset.UtcNow` for consistent UTC handling
- **Database**: PostgreSQL `NOW() AT TIME ZONE 'UTC'` ensures UTC timestamps
- **Benefits**: Eliminates timezone confusion and ensures consistent UTC storage

## Connection Strings
- **Development**: `Host=localhost;Database=GeminiCatalogDev;Username=postgres;Password=postgres`
- **Production**: `Host=localhost;Database=GeminiCatalog;Username=postgres;Password=postgres`

Make sure to update these connection strings according to your PostgreSQL setup.