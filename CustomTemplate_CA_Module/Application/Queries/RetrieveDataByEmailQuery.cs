using System.ComponentModel.DataAnnotations;

namespace CustomTemplate_CA_Module.Application.Queries;

public record class RetrieveDataByEmailQuery
(
    [Required, StringLength(255)] string Email
);
