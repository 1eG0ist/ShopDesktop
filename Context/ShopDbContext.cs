using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopDesktop.Models;

namespace ShopDesktop.Context;

public partial class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productstype> Productstypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=shopDB;Username=postgres;password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductCount)
                .HasDefaultValueSql("1")
                .HasColumnName("product_count");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(300)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductImg).HasColumnName("product_img");
            entity.Property(e => e.ProductName)
                .HasMaxLength(150)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ProductType).HasColumnName("product_type");
        });

        modelBuilder.Entity<Productstype>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("productstypes_pkey");

            entity.ToTable("productstypes");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.TypeName)
                .HasMaxLength(60)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(60)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("transactions_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.TransactionData)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transaction_data");
            entity.Property(e => e.TransactionProductCount)
                .HasDefaultValueSql("0")
                .HasColumnName("transaction_product_count");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("transactions_product_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserEmail, "users_user_email_key").IsUnique();

            entity.HasIndex(e => e.UserName, "users_user_name_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(60)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(30)
                .HasColumnName("user_password");
            entity.Property(e => e.UserRole)
                .HasDefaultValueSql("1")
                .HasColumnName("user_role");

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("users_user_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
