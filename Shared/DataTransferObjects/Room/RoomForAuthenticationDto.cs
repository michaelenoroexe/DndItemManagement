using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Room;

public record RoomForAuthenticationDto
{
    [Required(ErrorMessage = "Room id is required")]
    public int? Id { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    [Required(ErrorMessage = "Character id required")]
    public int? CharacterId { get; init; }
}
