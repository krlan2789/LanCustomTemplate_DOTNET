using CustomTemplate_CA_Module.Application.Dtos;
using CustomTemplate_CA_Module.Domain.Entities;

namespace CustomTemplate_CA_Module.Application.Factories;

public static class CustomTemplate_CA_DtoFactory
{
    public static CustomTemplate_CA_Dto ToDto(this CustomTemplate_CA_Entity entity)
    {
        return new CustomTemplate_CA_Dto
		(
            entity.Name,
            entity.Email,
            entity.PhoneNumber,
            entity.CreatedAt
        );
    }
}
