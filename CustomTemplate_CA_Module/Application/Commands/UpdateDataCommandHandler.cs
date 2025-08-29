using Core.Abstractions;
using CustomTemplate_CA_Module.Application.Abstractions;
using CustomTemplate_CA_Module.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomTemplate_CA_Module.Application.Commands;

public class UpdateDataCommandHandler(ILogger<ICustomTemplate_CA_WriteRepository> _logger, ICustomTemplate_CA_ReadRepository _readRepository, ICustomTemplate_CA_WriteRepository _writeRepository) : ICommandHandler<UpdateDataCommand>
{
    public async Task Handle(UpdateDataCommand command)
    {
        CustomTemplate_CA_Entity? currentData = await _readRepository.FindByEmailAsync(command.Email);
        if (currentData == null)
        {
            _logger.LogWarning("Data with email {Email} not found", command.Email);
            throw new InvalidOperationException($"Data with email {command.Email} not found.");
        }

        currentData
            .SetName(command.Name)
            .SetEmail(command.Email)
            .SetPhoneNumber(command.PhoneNumber);
        await _writeRepository.UpdateAsync(currentData);
    }
}
