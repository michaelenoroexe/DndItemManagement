namespace Shared.DataTransferObjects.CharacterItem; 

public abstract record CharacterItemForPatchDto 
{
    public int? ItemNumber { get; init; }

    public float? CurrentDurability { get; init; }
}
