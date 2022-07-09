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
    public class CategoryController : ControllerBase
    {
        private TEA_MILKContext _context;
        public CategoryController(TEA_MILKContext context)
        {
            _context = context;
        
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Categories.ToListAsync();
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<Category>> GetCategoryById(int id)
        {   
            
            var category  = await _context.Categories.FindAsync(id);         
            return  category!=null?category:NotFound() ;
        }
     }
}
