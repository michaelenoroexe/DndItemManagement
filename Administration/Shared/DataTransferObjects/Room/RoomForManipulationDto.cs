using System.ComponentModel.DataAnnotations;

namespace Administration.Shared.DataTransferObjects.Room;

public abstract record RoomForManipulationDto
{
    [Required(ErrorMessage = "Room name is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Login is 20 characters.")]
    public string? Name { get; init; }

    [MaxLength(25, ErrorMessage = "Maximum length for the Password is 25 characters.")]
    public string? Password { get; init; }
}
