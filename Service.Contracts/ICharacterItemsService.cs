using Entities.Models;
using Shared.DataTransferObjects.CharacterItem;

namespace Service.Contracts;

public interface ICharacterItemsService
{
    Task<IEnumerable<CharacterItemDto>> GetCharacterItemsAsync(int characterId, bool trackChanges);
    Task<CharacterItemDto> CreateCharacterItemAsync(int characterId, int itemId,
        CharacterItemForCreationDto chItemForCreation, bool trackChanges);
    Task DeleteCharacterItemAsync(int characterId, int itemId, bool trackChanges);
    Task UpdateCharacterItemAsync(int characterId, int itemId,
    CharacterItemForUpdateDto chItemForUpdate, bool trackChanges);
    Task<(CharacterItemForUpdateDto chItemToPatch, CharacterItem chItemEntity)> GetCharacterItemForPatchAsync(
        int characterId, int itemId, bool trackChanges);
    Task SaveChangesForPatchAsync(CharacterItemForUpdateDto chItemToPatch, CharacterItem chItemEntity);
}
