using Entities.Models;

namespace Repository.Contracts;

public interface ICharacterItemRepository
{
    Task<CharacterItem?> GetCharacterItemAsync(int characterId, int itemId, bool trackChanges);
    Task<IEnumerable<CharacterItem>> GetCharacterItemsAsync(int characterId, bool trackChanges);
    void CreateCharacterItem(int characterId, int itemId, CharacterItem characterItem);
    void DeleteCharacter(CharacterItem characterItem);
}
