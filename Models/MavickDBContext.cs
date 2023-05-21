using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MavickBackend.Models
{
    public partial class MavickDBContext : DbContext
    {
        public MavickDBContext()
        {
        }

        public MavickDBContext(DbContextOptions<MavickDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; } = null!;
        public virtual DbSet<Concept> Concepts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<SizeProduct> SizeProducts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAddress> UserAddresses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=MiCHAEL\\SQLEXPRESS;database=MavickDB;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Direction)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direction");

                entity.Property(e => e.Postalcode).HasColumnName("postalcode");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.IdUser).HasColumnName("idUser");


            });

            modelBuilder.Entity<Cart>(entity =>
            {

                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Product");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.ToTable("CategoryProduct");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryProduct_Category");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryProduct_Product");
            });

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.ToTable("Concept");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdSale).HasColumnName("idSale");

                entity.Property(e => e.Import)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("import");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Concept_Product");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdSale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Concept_Sale");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("total");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_User");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SizeProduct>(entity =>
            {
                entity.ToTable("SizeProduct");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdSize).HasColumnName("idSize");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.SizeProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SizeProduct_Product");

                entity.HasOne(d => d.IdSizeNavigation)
                    .WithMany(p => p.SizeProducts)
                    .HasForeignKey(d => d.IdSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SizeProduct_Size");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "Email_Unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.ToTable("UserAddress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAddress).HasColumnName("idAddress");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_Address");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
