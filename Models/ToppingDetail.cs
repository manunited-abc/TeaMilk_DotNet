using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("toppingDetail")]
    public partial class ToppingDetail
    {
        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }
        [Key]
        [Column("toppingId")]
        public int ToppingId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("ToppingDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ToppingId))]
        [InverseProperty("ToppingDetails")]
        public virtual Topping Topping { get; set; }
    }
}
