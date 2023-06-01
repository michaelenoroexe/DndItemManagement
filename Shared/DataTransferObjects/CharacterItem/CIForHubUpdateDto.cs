using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.CharacterItem;

public record CIForHubUpdateDto : CharacterItemForUpdateDto
{
    [Required(ErrorMessage = "Character id is a required field.")]
    public int CharacterId { get; init; }
    [Required(ErrorMessage = "Item id is a required field.")]
    public int ItemId { get; init; }
}
