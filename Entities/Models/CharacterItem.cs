namespace Entities.Models;

public class CharacterItem
{
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;
    public int ItemNumber { get; set; } = 0;
    public float CurrentDurability { get; set; } = 1.0f;
}
