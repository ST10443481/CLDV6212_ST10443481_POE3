using ABCRetailers.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCRetailers.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("PasswordHash");
            entity.Property(u => u.Role).IsRequired().HasMaxLength(20).HasDefaultValue("Customer");
        });
    }
}
