# GeminiCatalog DataSeeder

This project provides database seeding functionality for the GeminiCatalog application with realistic clothing product data.

## Overview

The DataSeeder creates sample data including:
- **8 Categories**: Men's Clothing, Women's Clothing, Shoes, Accessories, Outerwear, Activewear, Formal Wear, and Casual Wear
- **25+ Products**: Realistic clothing items with detailed descriptions and appropriate pricing
- **Product-Category Relationships**: Products are associated with relevant categories

## Sample Data Categories

### Clothing Categories
- **Men's Clothing**: Shirts, jeans, sweaters, blazers, etc.
- **Women's Clothing**: Blouses, dresses, cardigans, etc.
- **Outerwear**: Jackets, coats, trench coats
- **Activewear**: Athletic wear and performance clothing
- **Formal Wear**: Business attire and dress clothing
- **Casual Wear**: Everyday comfortable clothing

### Other Categories
- **Shoes**: Dress shoes, sneakers, boots, athletic shoes
- **Accessories**: Belts, scarves, watches, bags

## Usage

### Prerequisites
- Ensure PostgreSQL is running and accessible
- Database connection string is configured in `appsettings.json`
- Run any pending Entity Framework migrations first

### Running the Seeder

#### Basic Seeding
```bash
dotnet run --project src/GeminiCatalog.DataSeeder
```

This will:
1. Check if the database exists and create it if necessary
2. Skip seeding if data already exists
3. Create all categories first
4. Create products and associate them with appropriate categories
5. Log the progress and results

#### Clear Existing Data
```bash
dotnet run --project src/GeminiCatalog.DataSeeder -- --clear
```

This will remove all existing products and categories from the database.

#### Clear and Re-seed
```bash
# First clear existing data
dotnet run --project src/GeminiCatalog.DataSeeder -- --clear

# Then seed with fresh data
dotnet run --project src/GeminiCatalog.DataSeeder
```

## Configuration

The seeder uses the same connection string as the main API project. Update `appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "GeminiCatalogDbContext": "Host=localhost;Port=5432;Database=geminicatalog;Username=postgres;Password=YourPassword"
  }
}
```

## Sample Products

The seeder includes realistic products such as:
- Classic White Oxford Shirt ($89.99)
- Silk Wrap Blouse ($189.99)
- Leather Oxford Shoes ($199.99)
- Down Puffer Jacket ($249.99)
- Athletic Leggings ($79.99)

Each product includes:
- Realistic name and detailed description
- Market-appropriate pricing
- Proper categorization (products can belong to multiple categories)

## Logging

The seeder provides detailed logging including:
- Progress updates during seeding
- Success/failure messages
- Count of created items
- Warning messages for any issues

## Error Handling

The seeder includes robust error handling:
- Validates product prices using domain rules
- Checks for existing data to prevent duplicates
- Logs warnings for any failed creations
- Graceful handling of database connection issues

## Integration with Main Application

The seeder project references:
- `GeminiCatalog.Domain` - For entity creation and business rules
- `GeminiCatalog.Infrastructure` - For database context and persistence

This ensures the seeded data follows all domain rules and business constraints.