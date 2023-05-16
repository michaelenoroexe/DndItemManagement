namespace Entities.Models;

public class Action 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Item> Items { get; set; }
}
