using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.CharacterItem; 

public abstract record CharacterItemForManipulateDto 
{
    public int Number { get; init; } = 0;

    public float CurrentDurability { get; init; } = 0.0f;
}
