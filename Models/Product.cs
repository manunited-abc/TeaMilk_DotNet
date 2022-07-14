using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("products")]
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ToppingDetails = new HashSet<ToppingDetail>();
        }

        [Key]
        [Column("productId")]
        public int ProductId { get; set; }
        [Column("nameProduct")]
        [StringLength(100)]
        public string NameProduct { get; set; }
        [Column("price")]
        public float? Price { get; set; }
        [Column("categoryId")]
        public int? CategoryId { get; set; }
        [Column("images")]
        [StringLength(500)]
        public string Images { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [InverseProperty(nameof(ToppingDetail.Product))]
        public virtual ICollection<ToppingDetail> ToppingDetails { get; set; }
    }
}
