using Cart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;

namespace Cart.Infrastructure;

public class CartDbContext : DbContext
{
    public DbSet<Domain.Entities.Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Ignore<Product>();
        
        modelBuilder.Entity<Domain.Entities.Cart>(entity =>
        {
            entity.ToTable("carts");
            
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("id");

            entity.Property(c => c.UserId).HasColumnName("user_id")
                .IsRequired().HasMaxLength(255);
            
            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            entity.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at");
            
            entity.HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("cart_items");

            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).HasColumnName("id");

            entity.Property(i => i.CartId).HasColumnName("cart_id").IsRequired();
            entity.Property(i => i.ProductId).HasColumnName("product_id").IsRequired();
            entity.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .IsRequired()
                .HasDefaultValue(1) 
                .HasAnnotation("MinValue", 1);             
            
            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            entity.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at");

            entity.HasIndex(i => new { i.CartId, i.ProductId }).IsUnique();
        });
    }
}