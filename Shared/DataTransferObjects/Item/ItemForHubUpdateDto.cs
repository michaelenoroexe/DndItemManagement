using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Item;

public record ItemForHubUpdateDto : ItemForUpdateDto
{
    [Required(ErrorMessage = "Item id is a required field.")]
    public int? Id { get; init; }
    [Required(ErrorMessage = "Room id is a required field.")]
    public int? RoomId { get; init; }
}
