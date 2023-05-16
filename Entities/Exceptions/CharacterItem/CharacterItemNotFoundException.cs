namespace Entities.Exceptions.CharacterItem;

public class CharacterItemNotFoundException : NotFoundException
{
	public CharacterItemNotFoundException(int characterId, int itemId)
		: base($"Character with id: {characterId} doesn't have item with id: {itemId} in the database.") { }
}
