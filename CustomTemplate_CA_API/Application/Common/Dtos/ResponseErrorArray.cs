namespace CustomTemplate_CA_API.Application.Common.Dtos;

public record class ResponseErrorArray<T>(string Message, T[]? Errors);