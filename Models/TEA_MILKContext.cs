using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TeaMilk.Models
{
    public partial class TEA_MILKContext : DbContext
    {
        public TEA_MILKContext()
        {
        }

        public TEA_MILKContext(DbContextOptions<TEA_MILKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<ToppingDetail> ToppingDetails { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HIEU-ITNLU\\\\\\\\SQLEXPRESS,1433;Initial Catalog=TEA_MILK;User ID=kh;Password=hello");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__orders__userId__403A8C7D");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("pk_orderDetail");

                entity.Property(e => e.Size).IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderDeta__order__4AB81AF0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderDeta__produ__4BAC3F29");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__products__catego__38996AB5");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Toppings)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__toppings__catego__3D5E1FD2");
            });

            modelBuilder.Entity<ToppingDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ToppingId })
                    .HasName("pk_toppingDetail");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ToppingDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__toppingDe__order__4316F928");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.ToppingDetails)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__toppingDe__toppi__440B1D61");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__userInfo__CB9A1CFF065A8720");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Pass).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
