namespace Entities.Exceptions.Character;

public class CharacterNotFoundException : NotFoundException 
{
	public CharacterNotFoundException(int characterId)
		: base($"The Character with id: {characterId} doesn't exist in the database.") { }
}
