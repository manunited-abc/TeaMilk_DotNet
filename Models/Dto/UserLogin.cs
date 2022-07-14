using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeaMilk_DotNet.Models.Dto
{
    public class UserLogin
    {
        public UserLogin()
        {
           
        }

        public string NameUser { get; set; }
        public string Password { get; set; }
        
    }
}