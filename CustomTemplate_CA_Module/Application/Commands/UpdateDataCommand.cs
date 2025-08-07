using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_Module.Application.Commands;

public record class UpdateDataCommand
(
    [Required, MinLength(4), MaxLength(255)] string Email,
    [MaxLength(255)] string? Name = null,
    [MaxLength(32)] string? PhoneNumber = null
);
