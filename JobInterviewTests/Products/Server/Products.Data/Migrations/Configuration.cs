namespace Products.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Products.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ProductsDbContext context)
        {
            this.SeedProductTypes(context);
            this.SeedProducts(context);
        }

        private void SeedProductTypes(ProductsDbContext context)
        {
            if (context.ProductTypes.Any())
            {
                return;
            }

            var productTypes = new List<ProductType>();
            productTypes.Add(new ProductType() { Description = "Type 1" });
            productTypes.Add(new ProductType() { Description = "Type 2" });

            context.ProductTypes.AddOrUpdate(productTypes.ToArray());
            context.SaveChanges();
        }

        private void SeedProducts(ProductsDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var types = context.ProductTypes.Select(t => t.Id).ToArray();

            var products = new List<Product>();
            products.Add(new Product()
            {
                UserId = 1,
                TypeId = types[0],
                Description = "Product 1"
            });

            products.Add(new Product()
            {
                UserId = 3,
                TypeId = types[0],
                Description = "Product 2"
            });

            products.Add(new Product()
            {
                UserId = 2,
                TypeId = types[0],
                Description = "Product 3"
            });

            products.Add(new Product()
            {
                UserId = 1,
                TypeId = types[1],
                Description = "Product 4"
            });

            products.Add(new Product()
            {
                UserId = 2,
                TypeId = types[0],
                Description = "Product 5"
            });

            products.Add(new Product()
            {
                UserId = 1,
                TypeId = types[1],
                Description = "Product 6"
            });

            context.Products.AddOrUpdate(products.ToArray());
            context.SaveChanges();
        }
    }
}
