using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("orderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }
        [Key]
        [Column("productId")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [Column("size")]
        [StringLength(10)]
        public string Size { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; }
    }
}
