using CustomTemplate.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CustomTemplate.API.Data;

public class CustomTemplateDatabaseContext(DbContextOptions<CustomTemplateDatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<UserSessionLog> UserSessionLogs => Set<UserSessionLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users Table
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasOne(e => e.Profile).WithOne(r => r.User).HasForeignKey<UserProfile>(r => r.UserId);
            // entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            // entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
        });

        // UserProfiles Table
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.HasOne(e => e.User).WithOne(r => r.Profile).HasForeignKey<UserProfile>(r => r.UserId);
        });

        // UserSessionLog Table
        modelBuilder.Entity<UserSessionLog>(entity =>
        {
            entity.HasOne(e => e.User).WithMany().HasForeignKey(r => r.UserId);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}
