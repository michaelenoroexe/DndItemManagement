namespace Administration.Entities.Models; 

public class Room 
{ 
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Started { get; set; } = false;

    public int DmId { get; set; }
    public DM DM { get; set; } = null!;
}
