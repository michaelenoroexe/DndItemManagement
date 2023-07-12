namespace Administration.Entities.Models;

public class DM
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public ICollection<Room>? Rooms { get; set; }
}
