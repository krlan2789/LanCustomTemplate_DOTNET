using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_API.Application.UserDomain.Queries;

public record class UserProfileByUsernameQuery
(
    [Required, StringLength(128)] string Username
);
