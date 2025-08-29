using Core.Abstractions;
using CustomTemplate_CA_Module.Domain.Entities;

namespace CustomTemplate_CA_Module.Application.Abstractions;

public interface ICustomTemplate_CA_WriteRepository : IBaseWriteRepository
{
	public Task UpdateAsync(CustomTemplate_CA_Entity entity);
	public Task DeleteByPhoneNumberAsync(string phoneNumber);
	public Task DeleteByEmailAsync(string email);
}
