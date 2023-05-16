namespace Entities.Models;

public class ItemCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Item>? Items { get; set; }
}
