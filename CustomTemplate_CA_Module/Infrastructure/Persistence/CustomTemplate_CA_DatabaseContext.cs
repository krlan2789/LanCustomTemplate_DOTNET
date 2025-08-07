using CustomTemplate_CA_Module.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_Module.Infrastructure.Persistence;

public class CustomTemplate_CA_DatabaseContext(DbContextOptions<CustomTemplate_CA_DatabaseContext> options) : DbContext(options)
{
    public DbSet<CustomTemplate_CA_Entity> CustomTemplate_CA_s => Set<CustomTemplate_CA_Entity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomTemplate_CA_Entity>(entity =>
        {
            entity.HasQueryFilter(e => e.DeletedAt == null);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
        });
    }
}
