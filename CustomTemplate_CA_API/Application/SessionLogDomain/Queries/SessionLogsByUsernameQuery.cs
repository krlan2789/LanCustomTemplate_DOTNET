using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.SessionLogDomain.Queries;

public record class SessionLogsByUsernameQuery
(
    [Required, StringLength(128)] string Username
);