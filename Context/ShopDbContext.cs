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

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Productstype> Productstypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=shopDB;Username=postgres;password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.IsNew)
                .HasDefaultValueSql("true")
                .HasColumnName("is_new");
            entity.Property(e => e.NotificationAuthor).HasColumnName("notification_author");
            entity.Property(e => e.NotificationData)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("notification_data");
            entity.Property(e => e.NotificationText)
                .HasMaxLength(300)
                .HasColumnName("notification_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductAuthor).HasColumnName("product_author");
            entity.Property(e => e.ProductCount)
                .HasDefaultValueSql("1")
                .HasColumnName("product_count");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(300)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(150)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ProductType).HasColumnName("product_type");

            entity.HasOne(d => d.ProductAuthorNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductAuthor)
                .HasConstraintName("products_product_author_fkey");

            entity.HasOne(d => d.ProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductType)
                .HasConstraintName("products_product_type_fkey");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProdImgId).HasName("product_images_pkey");

            entity.ToTable("product_images");

            entity.Property(e => e.ProdImgId).HasColumnName("prod_img_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductImg).HasColumnName("product_img");
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
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserEmail, "users_user_email_key").IsUnique();

            entity.HasIndex(e => e.UserName, "users_user_name_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
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
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("user_roles_pkey");

            entity.ToTable("user_roles");

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_roles_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_roles_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
