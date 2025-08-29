using Core.Abstractions;
using CustomTemplate_CA_Module.Domain.Entities;

namespace CustomTemplate_CA_Module.Application.Abstractions;

public interface ICustomTemplate_CA_ReadRepository : IBaseReadRepository
{
	public Task<CustomTemplate_CA_Entity?> FindByEmailAsync(string email);
	public Task<bool> ExistsByEmailAsync(string email);
}
