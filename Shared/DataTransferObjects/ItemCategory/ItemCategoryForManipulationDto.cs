using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.ItemCategory;

public abstract record ItemCategoryForManipulationDto
{
    [Required(ErrorMessage = "Item category name is a required field.")]
    [MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
    public string? Name { get; init; }
}
