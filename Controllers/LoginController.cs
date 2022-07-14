using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TeaMilk.Models;
using TeaMilk.Models.Dto;
using TeaMilk_DotNet.Models.Dto;

namespace TeaMilk_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private TEA_MILKContext _context;

        public LoginController(IConfiguration config, TEA_MILKContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet("ok")]
        public IActionResult GetOk()
        {

            return Ok("ok5550");


        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            
            if (user != null)
            {
                var token = Generate(user);
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true
                });
                Jwt.setJwt(token);
                return Ok(token);
            }

            return NotFound("Invalid credentials");
        }

        private string Generate(UserInfo user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.NameUser),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserInfo Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.UserInfos.FirstOrDefault(o => o.NameUser.ToLower() == userLogin.NameUser.ToLower() && o.Pass.ToLower() == userLogin.Password.ToLower());

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}