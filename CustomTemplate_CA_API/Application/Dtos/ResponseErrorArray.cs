namespace CustomTemplate_CA_API.Application.Dtos;

public record class ResponseErrorArray<T>(string Message, T[]? Errors);