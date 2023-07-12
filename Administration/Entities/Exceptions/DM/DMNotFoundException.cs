namespace Administration.Entities.Exceptions.DM;

public class DMNotFoundException : NotFoundException
{
	public DMNotFoundException(int dmId)
		: base($"The DM with id: {dmId} doesn't exist in the database.") { }
}
