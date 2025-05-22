using CustomTemplate_CA_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CustomTemplate_CA_API.Infrastructure.Persistence;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserProfileEntity> UserProfiles => Set<UserProfileEntity>();
    public DbSet<UserSessionLogEntity> UserSessionLogs => Set<UserSessionLogEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users Table
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasOne(e => e.Profile).WithOne(r => r.User).HasForeignKey<UserProfileEntity>(r => r.UserId);
            // entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            // entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
        });

        // UserProfiles Table
        modelBuilder.Entity<UserProfileEntity>(entity =>
        {
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.HasOne(e => e.User).WithOne(r => r.Profile).HasForeignKey<UserProfileEntity>(r => r.UserId);
        });

        // UserSessionLog Table
        modelBuilder.Entity<UserSessionLogEntity>(entity =>
        {
            entity.HasOne(e => e.User).WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}
