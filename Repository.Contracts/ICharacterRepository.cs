using Entities.Models;

namespace Repository.Contracts;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetAllCharactersAsync(bool trackChanges);
    Task<IEnumerable<Character>> GetRoomCharactersAsync(int roomId, bool trackChanges);
    Task<Character?> GetCharacterAsync(int characterId, bool trackChanges);
    void CreateCharacter(int roomId, Character character);
    Task<IEnumerable<Character>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteCharacter(Character character);
}
