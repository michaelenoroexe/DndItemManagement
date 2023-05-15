using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Character;

public abstract record CharacterForManipulationDto
{
    [Required(ErrorMessage = "Character name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name { get; init; }
}
