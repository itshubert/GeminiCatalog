# Fresh Database Setup Script

## Step 1: Recreate Database (PostgreSQL)
Connect to PostgreSQL and run these commands to clean and recreate the database:

```sql
-- Drop the existing database (this removes ALL data and tables)
DROP DATABASE IF EXISTS geminicatalog;

-- Create a fresh database
CREATE DATABASE geminicatalog
    WITH OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;
```

## Step 2: Apply Fresh Migration
After recreating the database, run this command from the solution root:

```powershell
dotnet ef database update --project .\src\GeminiCatalog.Infrastructure\ --startup-project .\src\GeminiCatalog.Api\
```

## Expected Result
The migration should apply cleanly without errors and create:

### Tables Created:
1. **Categories** - With DateTimeOffset timestamps and UTC defaults
2. **Products** - With Price as decimal(18,2) and DateTimeOffset timestamps
3. **ProductCategories** - Junction table with composite primary key
4. **__EFMigrationsHistory** - EF Core migration tracking

### Key Features:
- ✅ **UTC Timestamps**: All dates use `timestamp with time zone` with `NOW() AT TIME ZONE 'UTC'`
- ✅ **Price Value Object**: Properly mapped to `numeric(18,2)` column
- ✅ **Many-to-Many**: Products ↔ Categories with cascade delete
- ✅ **Performance**: Index on CategoryId in junction table

## Verification
After applying the migration, you can verify with:

```sql
-- List all tables
\dt

-- Check table structures
\d "Products"
\d "Categories"  
\d "ProductCategories"

-- Verify migration history
SELECT * FROM "__EFMigrationsHistory";
```

You should see the migration `20250928024016_InitialCreate` listed as applied.