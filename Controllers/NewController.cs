using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeaMilk.Models;
namespace TeaMilk_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : Controller
    {   
        private TEA_MILKContext _context;

        public NewController(TEA_MILKContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public  IActionResult GetUsers()
        {
            try
            {
                var users = _context.UserInfos.ToList();
                if(users.Count == 0)
                {
                    return StatusCode(404, "No user found");

                }

                return Ok(users);

            }
            catch (Exception){
                return StatusCode(500, "An error has occurred");
            }
        }


    }
}