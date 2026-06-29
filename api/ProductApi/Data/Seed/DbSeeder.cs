using ProductApi.Models;

namespace ProductApi.Data.Seed;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new List<Product>
        {
            new Product { Name = "iPhone 15", Price = 999 },
            new Product { Name = "Samsung S24", Price = 899 },
            new Product { Name = "MacBook Pro", Price = 1999 }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}