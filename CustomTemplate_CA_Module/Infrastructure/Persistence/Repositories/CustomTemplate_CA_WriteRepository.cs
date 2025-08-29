using Core.Persistence;
using CustomTemplate_CA_Module.Application.Abstractions;
using CustomTemplate_CA_Module.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_Module.Infrastructure.Persistence.Repositories;

public class CustomTemplate_CA_WriteRepository(CustomTemplate_CA_DatabaseContext dbContext) : BaseWriteRepository<CustomTemplate_CA_DatabaseContext>(dbContext), ICustomTemplate_CA_WriteRepository
{
    public async Task UpdateAsync(CustomTemplate_CA_Entity entity)
    {
        var existingEntity = await _dbContext.CustomTemplate_CA_s
            .FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Entity of type {typeof(CustomTemplate_CA_Entity).Name} with ID {entity.Id} not found.");
        _dbContext.CustomTemplate_CA_s.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByPhoneNumberAsync(string username)
    {
        var user = await _dbContext.CustomTemplate_CA_s
            .FirstOrDefaultAsync(u => u.PhoneNumber == username)
            ?? throw new InvalidOperationException("CustomTemplate_CA_ not found");
        user.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(user);
    }

    public async Task DeleteByEmailAsync(string email)
    {
        var user = await _dbContext.CustomTemplate_CA_s
            .FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new InvalidOperationException("CustomTemplate_CA_ not found");
        user.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(user);
    }

    public async Task AddAsync(CustomTemplate_CA_Entity entity)
    {
        _dbContext.CustomTemplate_CA_s.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}
