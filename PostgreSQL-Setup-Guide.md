# PostgreSQL Setup Guide for GeminiCatalog

## Prerequisites
1. **Install PostgreSQL** (if not already installed)
   - Download from: https://www.postgresql.org/download/
   - Or use Docker: `docker run --name postgres -e POSTGRES_PASSWORD=yourpassword -p 5432:5432 -d postgres`

## Database Setup

### Option 1: Using Default PostgreSQL Setup
Update your connection strings in `appsettings.json` and `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "GeminiCatalogDbContext": "Host=localhost;Database=GeminiCatalogDev;Username=postgres;Password=yourpassword"
  }
}
```

### Option 2: Create Specific Database User
```sql
-- Connect to PostgreSQL as superuser and run:
CREATE USER geminicatalog WITH PASSWORD 'YourSecurePassword123!';
CREATE DATABASE "GeminiCatalogDev" OWNER geminicatalog;
CREATE DATABASE "GeminiCatalog" OWNER geminicatalog;
GRANT ALL PRIVILEGES ON DATABASE "GeminiCatalogDev" TO geminicatalog;
GRANT ALL PRIVILEGES ON DATABASE "GeminiCatalog" TO geminicatalog;
```

Then update connection strings:
```json
{
  "ConnectionStrings": {
    "GeminiCatalogDbContext": "Host=localhost;Database=GeminiCatalogDev;Username=geminicatalog;Password=YourSecurePassword123!"
  }
}
```

### Option 3: Using Docker PostgreSQL
```bash
# Start PostgreSQL in Docker
docker run --name geminicatalog-postgres \
  -e POSTGRES_USER=geminicatalog \
  -e POSTGRES_PASSWORD=YourSecurePassword123! \
  -e POSTGRES_DB=GeminiCatalogDev \
  -p 5432:5432 \
  -d postgres:15

# Connection string for Docker setup:
"Host=localhost;Database=GeminiCatalogDev;Username=geminicatalog;Password=YourSecurePassword123!"
```

## Apply Migrations

Once PostgreSQL is configured and running:

```powershell
# Apply migrations to create the database schema
dotnet ef database update --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

## Verify Database Creation

Connect to your PostgreSQL database and verify the tables were created:

```sql
-- List all tables
\dt

-- Check Products table structure  
\d "Products"

-- Check Categories table structure
\d "Categories"

-- Check ProductCategories junction table
\d "ProductCategories"
```

## Expected Tables

After successful migration, you should see:

1. **Products** - Contains product information with Price column (decimal)
2. **Categories** - Contains category information  
3. **ProductCategories** - Junction table for many-to-many relationship
4. **__EFMigrationsHistory** - EF Core migration tracking table

## Troubleshooting

### Connection Issues
- Ensure PostgreSQL service is running
- Verify the port (default: 5432)
- Check username/password combinations
- Ensure the database exists

### Permission Issues
- Grant necessary privileges to the database user
- Check PostgreSQL `pg_hba.conf` for authentication settings

### Testing Connection
```bash
# Test connection using psql
psql -h localhost -U postgres -d postgres

# Or with custom user
psql -h localhost -U geminicatalog -d GeminiCatalogDev
```