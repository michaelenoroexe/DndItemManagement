using AutoMapper;
using Entities.Exceptions.Character;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.Character;

namespace Service;

internal sealed class CharacterService : ICharacterService
{
    private readonly IRepositoryManager repository;
    private readonly IMapper mapper;

    private async Task<Character> GetCharacterAndCheckIfItExists(int characterId, bool trackChanges)
    {
        var character = await repository.Character.GetCharacterAsync(characterId, trackChanges);
        if (character is null) throw new CharacterNotFoundException(characterId);

        return character;
    }

    public CharacterService(IRepositoryManager repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CharacterDto> CreateRoomCharacterAsync(int roomId, CharacterForCreationDto characterForCreation, bool trackChanges)
    {
        var characterEntity = mapper.Map<Character>(characterForCreation);

        repository.Character.CreateCharacter(roomId, characterEntity);
        await repository.SaveAsync();

        var characterToReturn = mapper.Map<CharacterDto>(characterEntity);

        return characterToReturn;
    }

    public async Task DeleteCharacterAsync(int roomId, int id, bool trackChanges)
    {
        var characterDb = await GetCharacterAndCheckIfItExists(id, trackChanges);

        repository.Character.DeleteCharacter(characterDb);
        await repository.SaveAsync();
    }

    public async Task<IEnumerable<CharacterDto>> GetRoomCharacters(int roomId, bool trackChanges)
    {
        var charactersDb = await repository.Character.GetRoomCharactersAsync(roomId, trackChanges);

        var charactersToReturn = mapper.Map<IEnumerable<CharacterDto>>(charactersDb);

        return charactersToReturn;
    }

    public async Task UpdateCharacterAsync(int roomId, int id, CharacterForUpdateDto characterForUpdate, 
        bool roomTrackChanges, bool characterTrackChanges)
    {
        var characterDb = await GetCharacterAndCheckIfItExists(id, characterTrackChanges);

        mapper.Map(characterForUpdate, characterDb);
        await repository.SaveAsync();
    }

    public async Task<CharacterDto> GetCharacterAsync(int id, bool trackChanges)
    {
        var character = await GetCharacterAndCheckIfItExists(id, false);

        var characterToReturn = mapper.Map<CharacterDto>(character);
        return characterToReturn;
    }
}
