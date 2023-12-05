using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContentController : ControllerBase
    {
        private DataContext context;
        public ContentController(DataContext dataContext)
        {
            context = dataContext;
        }
        [HttpGet("string")]
        public async Task<ProductBindingTarget> GetString()
        {
            Product p = await context.Products.FirstAsync();
            return new ProductBindingTarget()
            {
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            };
        }
        [HttpGet("object/{format?}")]
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public async Task<Product> GetObject()
        {
            return await context.Products.FirstAsync();
        }
        [HttpPost]
        [Consumes("application/json")]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }
        // [HttpPost]
        // [Consumes("application/xml")]
        // public string SaveProductXml(ProductBindingTarget product)
        // {
        //     return $"XML: {product.Name}";
        // }
    }
}