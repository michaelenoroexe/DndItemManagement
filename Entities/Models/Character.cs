namespace Entities.Models; 

public class Character 
{ 
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Currency { get; set; } = 0;

    public int RoomId { get; set; }

    public ICollection<Item>? Items { get; set; }
    public ICollection<CharacterItem>? CharacterItems { get; set; }
}
