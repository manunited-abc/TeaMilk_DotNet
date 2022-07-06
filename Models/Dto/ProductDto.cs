using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TeaMilk.Models.Dto
{
    public  class ProductDto
    {
        public ProductDto()
        {
           
        }

        public int ProductId { get; set; }

        public string NameProduct { get; set; }

        public float Price { get; set; }

        public int CategoryId { get; set; }

        public string Images { get; set; }

    
    }
}
