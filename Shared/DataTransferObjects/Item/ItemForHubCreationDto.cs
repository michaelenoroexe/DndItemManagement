using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Item;

public record ItemForHubCreationDto : ItemForCreationDto
{
    [Required(ErrorMessage = "RoomId is a required field.")]
    public int? RoomId { get; init; }
}
