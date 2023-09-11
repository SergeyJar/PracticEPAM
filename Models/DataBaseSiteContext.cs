using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PracticEPAM.Models;

public partial class DataBaseSiteContext : IdentityDbContext<IdentityUser>
{
    public DataBaseSiteContext()
    {
    }

    public DataBaseSiteContext(DbContextOptions<DataBaseSiteContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<LikesWithDislike> LikesWithDislikes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-99K79KN\\SQLEXPRESS;Database=DataBaseSiteF;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategories);

            entity.Property(e => e.Category1)
                .HasMaxLength(50)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EditDate).HasColumnType("datetime");
            entity.Property(e => e.IdUser).HasMaxLength(450);
            entity.Property(e => e.Text).HasMaxLength(1500);

            entity.HasOne(d => d.IdReviewNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdReview)
                .HasConstraintName("FK_Comments_Reviews");

        });

        modelBuilder.Entity<LikesWithDislike>(entity =>
        {
            entity.Property(e => e.IdUser).HasMaxLength(450);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.LikesWithDislikes)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_LikesWithDislikes_Product");

        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.IdCategoriesNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategories)
                .HasConstraintName("FK_Product_Categories");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("PK_Review");

            entity.Property(e => e.IdUser).HasMaxLength(450);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Product");
            
        });



        base.OnModelCreating(modelBuilder);
    }

   
}
