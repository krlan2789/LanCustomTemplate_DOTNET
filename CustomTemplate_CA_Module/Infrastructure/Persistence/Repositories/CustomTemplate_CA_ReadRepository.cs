using Core.Persistence;
using CustomTemplate_CA_Module.Application.Abstractions;
using CustomTemplate_CA_Module.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomTemplate_CA_Module.Infrastructure.Persistence.Repositories;

public class CustomTemplate_CA_ReadRepository(CustomTemplate_CA_DatabaseContext dbContext) : BaseReadRepository<CustomTemplate_CA_DatabaseContext>(dbContext), ICustomTemplate_CA_ReadRepository
{
	public async Task<CustomTemplate_CA_Entity?> FindByEmailAsync(string email)
	{
		return await _dbContext.CustomTemplate_CA_s
			.AsNoTracking()
			.Where(u => u.Email == email)
			.Select(u => u)
			.FirstOrDefaultAsync();
	}

	public async Task<bool> ExistsByEmailAsync(string email)
	{
		return await _dbContext.CustomTemplate_CA_s
			.AsNoTracking()
			.Where(u => u.Email == email)
			.AnyAsync();
	}
}
