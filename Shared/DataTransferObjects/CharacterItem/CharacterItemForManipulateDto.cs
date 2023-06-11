using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.CharacterItem; 

public abstract record CharacterItemForManipulateDto 
{
    public int ItemNumber { get; init; } = 0;

    public float CurrentDurability { get; init; } = 0.0f;
}
