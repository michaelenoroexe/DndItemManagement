using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Item;

public record ItemForManipulationDto
{
    [Required(ErrorMessage = "Item name is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
    public string? Name { get; init; }

    public int MaxDurability { get; init; }
    public int Price { get; init; }
    public float Weight { get; init; }

    [MaxLength(1000, ErrorMessage = "Maximum length for secret description is 1000 characters.")]
    public string? SecretItemDescription { get; init; }

    [MaxLength(1500, ErrorMessage = "Maximum length for item description is 1500 characters.")]
    public string? ItemDescription { get; init; }
}
