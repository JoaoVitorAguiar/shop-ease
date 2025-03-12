using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;
using Users.Domain.Entities;

namespace Users.Infrastructure;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users"); 

            entity.HasKey(u => u.Id).HasName("id"); 
        
            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at"); 
        
            entity.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at"); 
        
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name"); 
        
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasConversion(email => email.Value, value => new Email(value))
                .HasColumnName("email"); 
        
            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.HasIndex(u => u.Email).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}