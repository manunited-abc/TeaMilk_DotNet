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
    public class ToppingController : ControllerBase
    {
        private TEA_MILKContext _context;
        public ToppingController(TEA_MILKContext context)
        {
            _context = context;
        
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topping>>> GetTopping()
        {
            return await _context.Toppings.ToListAsync();
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<Topping>> GetToppingById(int id)
        {   
            
            var topping  = await _context.Toppings.FindAsync(id);         
            return  topping!=null?topping:NotFound() ;
        }
     }
}