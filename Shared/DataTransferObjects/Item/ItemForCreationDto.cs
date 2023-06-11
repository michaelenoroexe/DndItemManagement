using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Item;

public record ItemForCreationDto : ItemForManipulationDto
{
    [Required(ErrorMessage = "Item category is a required field.")]
    public int ItemCategoryId { get; init; }

    [Required(ErrorMessage = "RoomId is a required field.")]
    public int? RoomId { get; init; }
}
