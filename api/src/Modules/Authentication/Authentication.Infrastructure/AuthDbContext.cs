using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure;

public class AuthDbContext : DbContext
{
    public DbSet<UserCredentials> UserCredentials { get; set; }
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCredentials>(entity =>
        {
            entity.HasNoKey(); 
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
            
            entity.ToTable("users");
        });

        base.OnModelCreating(modelBuilder);
    }
}