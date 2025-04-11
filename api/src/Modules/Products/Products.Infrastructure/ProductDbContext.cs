using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infrastructure;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(p => p.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(p => p.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired()
                .HasColumnName("price");

            entity.Property(p => p.Stock)
                .IsRequired()
                .HasColumnName("stock");

            entity.Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("sku");
            
            entity.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            entity.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at");

            entity.HasIndex(p => p.Sku)
                .IsUnique();
            entity.HasIndex(p => p.Name)
                .IsUnique();

            entity.HasOne<Category>(u => u.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(u => u.CategoryId);
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("id");


            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(c => c.Slug)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("slug");
            
            entity.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            entity.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at");
            
            entity.HasIndex(p => p.Name)
                .IsUnique();
        });
    }
}