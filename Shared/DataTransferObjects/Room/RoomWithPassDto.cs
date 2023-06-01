using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Room;

public record RoomWithPassDto : RoomForManipulationDto 
{
    [Required(ErrorMessage = "Room password is a required field.")]
    [MaxLength(25, ErrorMessage = "Maximum length for the Password is 25 characters.")]
    public string? Password { get; init; }
}
