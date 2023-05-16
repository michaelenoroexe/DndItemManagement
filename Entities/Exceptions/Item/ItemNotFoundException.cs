namespace Entities.Exceptions.Item;

public class ItemNotFoundException : NotFoundException
{
	public ItemNotFoundException(int itemId)
		: base($"The Item with id: {itemId} doesn't exist in the database.") { }
}
