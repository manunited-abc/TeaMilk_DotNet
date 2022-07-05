using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaMilk.Models;

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
        [HttpGet("category/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int id)
        {
            return await _context.Products.Where(p => p.CategoryId == id).ToListAsync();
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product  = await _context.Products.FindAsync(id);
            return  product!=null?product:NotFound() ;
        }
     }
}
