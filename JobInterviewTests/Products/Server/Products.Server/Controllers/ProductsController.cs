namespace Products.Server.Controllers
{
    using Products.Data;
    using Products.Server.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class ProductsController : ApiController
    {
        private ProductsDbContext databaseContext;

        public ProductsController()
            : this(new ProductsDbContext())
        {
        }

        public ProductsController(ProductsDbContext context)
        {
            this.databaseContext = context;
        }

        [HttpGet]
        public IHttpActionResult GetByUserId(int userId)
        {
            var productsByUserId = this.databaseContext.Products
                    .Where(p => p.UserId == userId)
                    .Select(p => new ProductModel()
                    {
                        Id = p.Id,
                        Description = p.Description,
                        ProductType = p.Type.Description
                    })
                    .ToList();

            return Ok(productsByUserId);
        }
    }
}
