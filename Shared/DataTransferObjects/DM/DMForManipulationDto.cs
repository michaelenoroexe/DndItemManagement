using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.DM;

public abstract record RoomForManipulationDto
{
    [Required(ErrorMessage = "DM login is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Login is 20 characters.")]
    public string? Login { get; init; }

    [Required(ErrorMessage = "Password is a required field.")]
    [MaxLength(25, ErrorMessage = "Maximum length for the Password is 25 characters.")]
    public string? Password { get; init; }
}
