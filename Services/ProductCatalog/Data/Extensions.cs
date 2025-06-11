using System.Runtime.CompilerServices;

namespace ProductCatalog.Data
{
    public static class Extensions
    {
        public static void UseMigration(this WebApplication app )
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            context.Database.Migrate();
            DataSeeder.Seed(context);
        }
    }

    public class DataSeeder
    {
        public static void Seed(ProductDbContext context)
        {
            if (context.Products.Any())
                return;
            context.Products.AddRange(Products);
            context.SaveChanges();
        }

        public static IEnumerable<Product> Products => [
            new Product {ProductName="Techno", ProductDescription="An android mobile product from Japan", Price=150.00m, ProductImage="techno.png" },
            new Product {ProductName="Samsung", ProductDescription="An android mobile product from China", Price=170.00m, ProductImage="samsumg.png" },
            new Product {ProductName="IPhone", ProductDescription="IPhone mobile product from USA", Price=240.00m, ProductImage="iphone.png" },
            new Product {ProductName="Addidas", ProductDescription="Sport trainers product from USA", Price=50.00m, ProductImage="addidas.png" },
            new Product {ProductName="HP Laptop", ProductDescription="Laptop product from USA", Price=650.00m, ProductImage="addidas.png" }
            ];
    }
}
