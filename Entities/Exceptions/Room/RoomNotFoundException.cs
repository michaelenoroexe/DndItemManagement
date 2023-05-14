namespace Entities.Exceptions.Room;

public class RoomNotFoundException : NotFoundException
{
	public RoomNotFoundException(int roomId) 
		: base($"The Room with id: {roomId} doesn't exist in the database.") { }
}
