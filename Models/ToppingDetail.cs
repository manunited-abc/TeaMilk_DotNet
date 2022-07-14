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
        [Column("toppingId")]
        public int ToppingId { get; set; }
        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }
        [Key]
        [Column("productId")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("ToppingDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ToppingDetails")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(ToppingId))]
        [InverseProperty("ToppingDetails")]
        public virtual Topping Topping { get; set; }
    }
}
