namespace Entities.Models; 

public class Item
{ 
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int MaxDurability { get; set; } = 0;
    public int Price { get; set; } = 0;
    public float Weight { get; set; } = 0.0f;
    public string? SecretItemDescription { get; set; }
    public string? ItemDescription { get; set; }

    public int ItemCategoryId { get; set; }
    public ItemCategory ItemCategory { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public ICollection<Action>? Actions { get; set; }
    public ICollection<Character>? Characters { get; set; }
    public ICollection<CharacterItem>? CharactersItem { get; set; }
}
