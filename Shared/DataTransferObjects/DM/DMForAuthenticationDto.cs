using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.DM;

public record DMForAuthenticationDto 
{
    [Required(ErrorMessage = "Login is required")]
    public string? Login { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
}
