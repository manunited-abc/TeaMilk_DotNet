using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeaMilk.Models.Dto
{
    public  class UserInfoDto
    {
        public UserInfoDto()
        {
           
        }

       

        public string NameUser { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Pass { get; set; }

        public string Role { get; set; }

    
    }
}