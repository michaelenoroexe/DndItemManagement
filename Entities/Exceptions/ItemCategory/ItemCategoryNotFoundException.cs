using Entities.Models;

namespace Entities.Exceptions.ItemCategory;

public class ItemCategoryNotFoundException : NotFoundException
{
	public ItemCategoryNotFoundException(int itemCategoryId)
		: base($"The Item category with id: {itemCategoryId} doesn't exist in the database.") { }
}
