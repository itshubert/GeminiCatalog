using GeminiCatalog.Domain.Categories;
using GeminiCatalog.Domain.Common.Models;
using GeminiCatalog.Domain.Products;

namespace GeminiCatalog.DataSeeder.Data;

public static class SeedData
{
    public static readonly List<Category> Categories = new()
    {
        Category.Create("Men's Clothing", "Clothing and accessories for men"),
        Category.Create("Women's Clothing", "Clothing and accessories for women"),
        Category.Create("Shoes", "Footwear for all occasions"),
        Category.Create("Accessories", "Fashion accessories and jewelry"),
        Category.Create("Outerwear", "Jackets, coats, and outdoor wear"),
        Category.Create("Activewear", "Athletic and workout clothing"),
        Category.Create("Formal Wear", "Business and formal attire"),
        Category.Create("Casual Wear", "Everyday comfortable clothing")
    };

    public static readonly List<ProductSeedModel> Products = new()
    {
        // Men's Clothing
        new("Classic White Oxford Shirt", 
            "Timeless white oxford shirt made from premium cotton. Perfect for business or casual wear with its versatile design and comfortable fit.", 
            89.99m, 
            new[] { "Men's Clothing", "Formal Wear", "Casual Wear" }),
        
        new("Slim Fit Dark Wash Jeans", 
            "Modern slim-fit jeans in dark indigo wash. Crafted from stretch denim for comfort and style. Features classic five-pocket styling.", 
            124.99m, 
            new[] { "Men's Clothing", "Casual Wear" }),
        
        new("Merino Wool V-Neck Sweater", 
            "Luxurious merino wool sweater with classic V-neck design. Soft, breathable, and perfect for layering in any season.", 
            159.99m, 
            new[] { "Men's Clothing", "Casual Wear" }),
        
        new("Tailored Navy Blazer", 
            "Sophisticated navy blazer with modern tailored fit. Made from premium wool blend fabric with subtle texture and refined details.", 
            349.99m, 
            new[] { "Men's Clothing", "Formal Wear" }),
        
        new("Performance Polo Shirt", 
            "Athletic polo shirt with moisture-wicking technology. Perfect for golf, tennis, or casual wear with UV protection.", 
            74.99m, 
            new[] { "Men's Clothing", "Activewear", "Casual Wear" }),

        // Women's Clothing
        new("Silk Wrap Blouse", 
            "Elegant silk wrap blouse in soft cream color. Features flutter sleeves and a flattering wrap silhouette. Perfect for work or evening.", 
            189.99m, 
            new[] { "Women's Clothing", "Formal Wear" }),
        
        new("High-Waisted Skinny Jeans", 
            "Flattering high-waisted skinny jeans in classic blue wash. Made with stretch denim for comfort and features a sculpting fit.", 
            119.99m, 
            new[] { "Women's Clothing", "Casual Wear" }),
        
        new("Floral Midi Dress", 
            "Romantic floral midi dress with three-quarter sleeves. Features a fitted bodice and flowing skirt in beautiful botanical print.", 
            149.99m, 
            new[] { "Women's Clothing", "Casual Wear" }),
        
        new("Cashmere Cardigan", 
            "Luxurious cashmere cardigan in soft pearl gray. Button-front style with ribbed trim details. Essential layering piece.", 
            249.99m, 
            new[] { "Women's Clothing", "Casual Wear" }),
        
        new("Little Black Dress", 
            "Timeless little black dress with modern fit. Features subtle ruching at the waist and three-quarter sleeves. Perfect for any occasion.", 
            179.99m, 
            new[] { "Women's Clothing", "Formal Wear" }),

        // Shoes
        new("Leather Oxford Shoes", 
            "Classic leather oxford shoes in rich brown. Handcrafted with premium leather upper and comfortable cushioned sole.", 
            199.99m, 
            new[] { "Shoes", "Formal Wear", "Men's Clothing" }),
        
        new("White Leather Sneakers", 
            "Minimalist white leather sneakers with clean lines. Features premium leather upper and comfortable rubber sole.", 
            134.99m, 
            new[] { "Shoes", "Casual Wear" }),
        
        new("Black Heeled Ankle Boots", 
            "Stylish ankle boots with 3-inch heel. Made from supple leather with side zipper closure. Versatile for work or weekend.", 
            169.99m, 
            new[] { "Shoes", "Women's Clothing", "Formal Wear" }),
        
        new("Running Shoes", 
            "High-performance running shoes with advanced cushioning technology. Breathable mesh upper and responsive foam midsole.", 
            149.99m, 
            new[] { "Shoes", "Activewear" }),
        
        new("Canvas High-Top Sneakers", 
            "Classic canvas high-top sneakers in navy blue. Retro-inspired design with rubber toe cap and lace-up closure.", 
            79.99m, 
            new[] { "Shoes", "Casual Wear" }),

        // Accessories
        new("Leather Belt", 
            "Genuine leather belt with polished silver buckle. Classic width and timeless design suitable for dress or casual wear.", 
            64.99m, 
            new[] { "Accessories", "Men's Clothing" }),
        
        new("Silk Scarf", 
            "Luxurious silk scarf with artistic floral pattern. Versatile accessory that adds elegance to any outfit.", 
            89.99m, 
            new[] { "Accessories", "Women's Clothing" }),
        
        new("Stainless Steel Watch", 
            "Sophisticated stainless steel watch with minimalist design. Features sapphire crystal glass and water resistance.", 
            299.99m, 
            new[] { "Accessories" }),
        
        new("Leather Crossbody Bag", 
            "Compact leather crossbody bag in cognac brown. Perfect size for essentials with adjustable strap and interior pockets.", 
            124.99m, 
            new[] { "Accessories", "Women's Clothing" }),

        // Outerwear
        new("Down Puffer Jacket", 
            "Warm down-filled puffer jacket with water-resistant shell. Features removable hood and multiple pockets for winter protection.", 
            249.99m, 
            new[] { "Outerwear", "Casual Wear" }),
        
        new("Wool Peacoat", 
            "Classic wool peacoat in charcoal gray. Double-breasted design with anchor buttons and warm wool lining.", 
            299.99m, 
            new[] { "Outerwear", "Formal Wear" }),
        
        new("Denim Jacket", 
            "Vintage-inspired denim jacket in medium wash. Classic trucker style with button closure and chest pockets.", 
            99.99m, 
            new[] { "Outerwear", "Casual Wear" }),
        
        new("Trench Coat", 
            "Elegant trench coat in khaki with classic details. Features belt tie, storm flap, and button-out lining.", 
            329.99m, 
            new[] { "Outerwear", "Formal Wear" }),

        // Activewear
        new("Moisture-Wicking T-Shirt", 
            "Performance t-shirt with advanced moisture-wicking fabric. Lightweight and breathable for intense workouts.", 
            39.99m, 
            new[] { "Activewear", "Casual Wear" }),
        
        new("Athletic Leggings", 
            "High-performance leggings with four-way stretch fabric. Features side pockets and flatlock seams for comfort.", 
            79.99m, 
            new[] { "Activewear", "Women's Clothing" }),
        
        new("Zip-Up Hoodie", 
            "Comfortable zip-up hoodie in heather gray. Made from soft cotton blend with kangaroo pocket and drawstring hood.", 
            69.99m, 
            new[] { "Activewear", "Casual Wear" }),
        
        new("Compression Shorts", 
            "Athletic compression shorts with moisture-wicking technology. Four-way stretch fabric for optimal performance.", 
            49.99m, 
            new[] { "Activewear", "Men's Clothing" })
    };
}

public record ProductSeedModel(
    string Name,
    string Description,
    decimal Price,
    string[] CategoryNames);