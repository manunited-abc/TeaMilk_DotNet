using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeaMilk_DotNet.Models.Dto
{
    public class Jwt

   

    {
         private static String Jwtoken = "jwt in here";

         public Jwt()
        {
           
           
        }

        public static void setJwt(String jwt){
            Jwtoken = jwt;
        }

        public static String getJwt(){
            return Jwtoken;
        }


        
    }
}