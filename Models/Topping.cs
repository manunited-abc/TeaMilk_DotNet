using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("toppings")]
    public partial class Topping
    {
        public Topping()
        {
            ToppingDetails = new HashSet<ToppingDetail>();
        }

        [Key]
        [Column("toppingId")]
        public int ToppingId { get; set; }
        [Column("nameTopping")]
        [StringLength(100)]
        public string NameTopping { get; set; }
        [Column("price")]
        public float? Price { get; set; }

        [InverseProperty(nameof(ToppingDetail.Topping))]
        public virtual ICollection<ToppingDetail> ToppingDetails { get; set; }
    }
}
