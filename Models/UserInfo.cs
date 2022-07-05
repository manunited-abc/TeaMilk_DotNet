using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TeaMilk.Models
{
    [Table("userInfos")]
    public partial class UserInfo
    {
        public UserInfo()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("nameUser")]
        [StringLength(100)]
        public string NameUser { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("phone")]
        [StringLength(10)]
        public string Phone { get; set; }
        [Column("pass")]
        [StringLength(100)]
        public string Pass { get; set; }

        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
