using AutoMapper;
using Entities.Exceptions.Character;
using Entities.Exceptions.CharacterItem;
using Entities.Exceptions.Item;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.CharacterItem;

namespace Service;

internal sealed class CharacterItemsService : ICharacterItemsService
{
    private readonly IRepositoryManager repository;
    private readonly IMapper mapper;

    private async Task CheckIfCharacterExists(int characterId, bool trackChanges)
    {
        var character = await repository.Character.GetCharacterAsync(characterId, trackChanges);
        if (character is null) throw new CharacterNotFoundException(characterId);
    }
    private async Task CheckIfItemExists(int itemId, bool trackChanges)
    {
        var item = await repository.Character.GetCharacterAsync(itemId, trackChanges);
        if (item is null) throw new ItemNotFoundException(itemId);
    }
    private async Task<CharacterItem> GetCharacterItemAndCheckIfItExists(int characterId, int itemId, bool trackChanges)
    {
        var chItem = await repository.CharacterItem.GetCharacterItemAsync(characterId, itemId, trackChanges);
        if (chItem is null) throw new CharacterItemNotFoundException(characterId, itemId);

        return chItem;
    }

    public CharacterItemsService(IRepositoryManager repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CharacterItemDto> CreateCharacterItemAsync(int characterId, int itemId, 
        CharacterItemForCreationDto roomForCreation, bool trackChanges)
    {
        await CheckIfCharacterExists(characterId, trackChanges);
        await CheckIfItemExists(itemId, trackChanges);

        var chItemEntity = mapper.Map<CharacterItem>(roomForCreation);

        repository.CharacterItem.CreateCharacterItem(characterId, itemId, chItemEntity);
        await repository.SaveAsync();

        var chItemToReturn = mapper.Map<CharacterItemDto>(chItemEntity);

        return chItemToReturn;
    }

    public async Task DeleteCharacterItemAsync(int characterId, int itemId, bool trackChanges)
    {
        await CheckIfCharacterExists(characterId, trackChanges);
        await CheckIfItemExists(itemId, trackChanges);

        var chItemDb = await GetCharacterItemAndCheckIfItExists(characterId, itemId, trackChanges);

        repository.CharacterItem.DeleteCharacter(chItemDb);
        await repository.SaveAsync();
    }

    public async Task<IEnumerable<CharacterItemDto>> GetCharacterItemsAsync(int characterId, bool trackChanges)
    {
        await CheckIfCharacterExists(characterId, trackChanges);

        var chItemsDb = await repository.CharacterItem.GetCharacterItemsAsync(characterId, trackChanges);

        var CharacterItemToReturn = mapper.Map<IEnumerable<CharacterItemDto>>(chItemsDb);

        return CharacterItemToReturn;
    }

    public async Task<(CharacterItemForUpdateDto chItemToPatch, CharacterItem chItemEntity)>
        GetCharacterItemForPatchAsync(int characterId, int itemId, bool trackChanges)
    {
        await CheckIfCharacterExists(characterId, trackChanges);
        await CheckIfItemExists(itemId, trackChanges);

        var chItemDb = await GetCharacterItemAndCheckIfItExists(characterId, itemId, trackChanges);

        var chItemToPatch = mapper.Map<CharacterItemForUpdateDto>(chItemDb);

        return (chItemToPatch, chItemDb);
    }

    public async Task SaveChangesForPatchAsync(CharacterItemForUpdateDto characterItemToPatch, CharacterItem characterItemEntity)
    {
        mapper.Map(characterItemToPatch, characterItemEntity);
        await repository.SaveAsync();
    }

    public async Task UpdateCharacterItemAsync(int characterId, int itemId, 
        CharacterItemForUpdateDto characterItemForUpdate, bool trackChanges)
    {
        await CheckIfCharacterExists(characterId, trackChanges);
        await CheckIfItemExists(itemId, trackChanges);

        var chItemDb = await GetCharacterItemAndCheckIfItExists(characterId, itemId, trackChanges);

        mapper.Map(characterItemForUpdate, chItemDb);
        await repository.SaveAsync();
    }
}
