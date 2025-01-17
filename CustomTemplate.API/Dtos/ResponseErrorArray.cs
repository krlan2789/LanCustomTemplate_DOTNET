namespace CustomTemplate.API.Dtos;

public record class ResponseErrorArray<T>(string Message, T[]? Errors);