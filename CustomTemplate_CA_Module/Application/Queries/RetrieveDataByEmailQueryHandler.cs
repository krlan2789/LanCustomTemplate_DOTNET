using CustomTemplate_CA_Module.Application.Abstractions;
using CustomTemplate_CA_Module.Application.Dtos;
using CustomTemplate_CA_Module.Application.Factories;
using Microsoft.Extensions.Logging;

namespace CustomTemplate_CA_Module.Application.Queries;

public class RetrieveDataByEmailQueryHandler(ILogger<ICustomTemplate_CA_ReadRepository> _logger, ICustomTemplate_CA_ReadRepository _repository)
{
    public async Task<CustomTemplate_CA_Dto> Handle(RetrieveDataByEmailQuery query)
    {
        var currentData = await _repository.FindByEmailAsync(query.Email);
        if (currentData == null)
        {
            _logger.LogWarning("Data with email {Email} not found", query.Email);
            throw new InvalidOperationException($"Data with email {query.Email} not found.");
        }
        return currentData.ToDto();
    }
}
