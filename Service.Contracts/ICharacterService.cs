using Shared.DataTransferObjects.Character;

namespace Service.Contracts;

public interface ICharacterService
{
    Task<IEnumerable<CharacterDto>> GetRoomCharacters(int roomId, bool trackChanges);
    Task<CharacterDto> CreateRoomCharacterAsync(int roomId,
        CharacterForCreationDto characterForCreation, bool trackChanges);
    Task DeleteCharacterAsync(int roomId, int id, bool trackChanges);
    Task UpdateCharacterAsync(int roomId, int id,
        CharacterForUpdateDto characterForUpdate, bool roonTrackChanges, bool characterTrackChanges);
}
