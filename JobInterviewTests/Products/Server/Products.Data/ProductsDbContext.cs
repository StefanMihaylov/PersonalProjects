namespace Products.Data
{
    using System.Data.Entity;
    using Products.Data.Migrations;
    using Products.Data.Models;

    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext()
            : base("ProductDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsDbContext, Configuration>());
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<ProductType> ProductTypes { get; set; }
    }
}
