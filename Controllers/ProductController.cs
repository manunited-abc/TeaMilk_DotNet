using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaMilk.Models;
using TeaMilk.Models.Dto;

namespace TeaMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private TEA_MILKContext _context;

        public ProductController(TEA_MILKContext context)
        {
            _context = context;
        }

         [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context
                .Products
                .ToListAsync();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct(string search)
        {
            Console.WriteLine(search);
            var products = from p in _context.Products join c in _context.Categories on p.CategoryId equals c.CategoryId 
            where  p.NameProduct.Contains(search) ||  c.NameCategory.Contains(search) select p;
            return await products.ToListAsync();
        }
        [HttpGet("category/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int id)
        {
            return await _context
                .Products
                .Where(p => p.CategoryId == id)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product != null ? product : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            var product = new Product{
                NameProduct = productDto.NameProduct,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                Images = productDto.Images
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProductById",
            new { id = product.ProductId },
            productDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}