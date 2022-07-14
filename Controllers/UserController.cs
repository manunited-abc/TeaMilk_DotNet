using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaMilk.Models;
using TeaMilk.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace TeaMilk_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]


     
    public class UserController : ControllerBase
    {   
        
        private TEA_MILKContext _context;

        public UserController(TEA_MILKContext context)
        {
            _context = context;
        }

         [HttpGet("ok")]
        public  IActionResult Get()
        {

            
            return Ok("ok");

            
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfoById(int id)
        {
            var userInfo = await _context.UserInfos.FindAsync(id);
            return userInfo != null ? userInfo : NotFound();
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<UserInfoDto>> PostUserInfo(UserInfoDto userInforDto)
        {   
            try
            {  var userInfo = new UserInfo{
                NameUser = userInforDto.NameUser,
                Email = userInforDto.Email,
                Phone = userInforDto.Phone,
                Pass = userInforDto.Pass,
                Role = userInforDto.Role
                };

                var userByName =  _context.UserInfos.FirstOrDefault(x => x.NameUser == userInfo.NameUser);
                if (userByName != null)
                {
                    return BadRequest("UserName da ton tai");
                }
                var userByEmail =  _context.UserInfos.FirstOrDefault(x => x.Email == userInfo.Email);
                if (userByEmail != null)
                {
                    return BadRequest("Email da dang ky");
                }
                 var userByPhone =  _context.UserInfos.FirstOrDefault(x => x.Phone == userInfo.Phone);
                if (userByPhone != null)
                {
                    return BadRequest("Phone da dang ky");
                }
                _context.UserInfos.Add(userInfo);
                 await _context.SaveChangesAsync();
                return Created("Thanh cong",userInfo);
            }
            catch(Exception ex){
                return BadRequest("Khong thanh cong");

            }
          
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var userInfo = await _context.UserInfos.FindAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            _context.UserInfos.Remove(userInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPut("updateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UserInfo newUserInfo)
        {
           
            try
            {
                var userInfo =  _context.UserInfos.FirstOrDefault(x => x.UserId == newUserInfo.UserId);
                if (userInfo == null)
                {
                    return StatusCode(404, "User not found");
                }

                userInfo.UserId = newUserInfo.UserId;
                userInfo.NameUser = newUserInfo.NameUser;
                userInfo.Email = newUserInfo.Email;
                userInfo.Phone = newUserInfo.Phone;
                userInfo.Pass = newUserInfo.Pass;


                
                await _context.SaveChangesAsync();
 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occured");

            }
            return NoContent();
          
        }
        
       


    }
}