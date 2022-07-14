using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaMilk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeaMilk.Models.Dto;
using TeaMilk_DotNet.Models.Dto;

namespace TeaMilk_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private IConfiguration _configuration;
        private TEA_MILKContext _context;

        public TokenController(IConfiguration configuration, TEA_MILKContext context){
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async  Task<IActionResult> Get()
        {

            var  jwt =  Request.Cookies["jwt"];
            String token = Jwt.getJwt();

            return Ok(token);

            
        }
        
        
        // [HttpPost]
        // public async Task<IActionResult> Post(UserInfoDto userInfoDto)
        // {   
        //     // if(userInfoDto != null && userInfoDto.NameUser != null && userInfoDto.Pass != null){
        //     // // var user = await GetUser(userInfoDto.NameUser,userInfoDto.Pass);
        //     // var user = userInfoDto;
        //     // if(user != null)
        //     // {
        //     //     // var claims = new[]{
        //     //     //     new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
        //     //     //     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        //     //     //     new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
        //     //     //     new Claim("Id",user.UserId.ToString()),
        //     //     //     new Claim(ClaimTypes.NameIdentifier,user.NameUser),
        //     //     //     new Claim(ClaimTypes.Email,user.Email),
        //     //     //     new Claim(ClaimTypes.Role,user.Role)

        //     //     // };
        //     //     // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //     //     // var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //     //     // var token = new JwtSecurityToken(
        //     //     // _configuration["Jwt:Issuer"],
        //     //     // _configuration["Jwt:Audience"],
        //     //     // claims,
        //     //     // expires: DateTime.Now.AddMinutes(20),
        //     //     // signingCredentials: signIn);

        //     //     // return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //     //     return Ok("ok");
            
        //     // }
        //     // else
        //     // {
        //         return BadRequest("Invalid credentials");
        //     // }
        //     // }
        //     // else{
        //     //     return BadRequest("haha");

        //     // }
            
        // }
        // [HttpGet]
        // public async Task<UserInfo> GetUser(string userName, string pass){
        //     return await _context.UserInfos.FirstOrDefaultAsync(u=> u.NameUser==userName && u.Pass == pass);
        // }
       [HttpPost]
        public async Task<IActionResult> Post(UserInfo _userData)
        {
 
            if (_userData != null && _userData.Email != null && _userData.Pass != null)
            {
                var user = await GetUser(_userData.Email, _userData.Pass);
 
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("UserName", user.NameUser),
                    new Claim("Email", user.Email),
                    new Claim("Role", user.Role)
                   };
 
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
 
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
 
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
 
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
 
        private async Task<UserInfo> GetUser(string email, string password)
        {
            return await _context.UserInfos.FirstOrDefaultAsync(u => u.Email == email && u.Pass == password);
        }

        
    }
}