using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("orders")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ToppingDetails = new HashSet<ToppingDetail>();
        }

        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }
        [Column("createDate", TypeName = "date")]
        public DateTime? CreateDate { get; set; }
        [Column("totalPrice")]
        public float? TotalPrice { get; set; }
        [Column("addressShip")]
        [StringLength(500)]
        public string AddressShip { get; set; }
        [Column("note")]
        [StringLength(500)]
        public string Note { get; set; }
        [Column("userId")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserInfo.Orders))]
        public virtual UserInfo User { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [InverseProperty(nameof(ToppingDetail.Order))]
        public virtual ICollection<ToppingDetail> ToppingDetails { get; set; }
    }
}
