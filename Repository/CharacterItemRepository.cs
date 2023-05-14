using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class CharacterItemRepository : RepositoryBase<CharacterItem>, ICharacterItemRepository
{
    public CharacterItemRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext) { }

    public void CreateCharacterItem(int characterId, int itemId, CharacterItem characterItem)
    {
        characterItem.CharacterId = characterId;
        characterItem.ItemId = itemId;
        Create(characterItem);
    }

    public void DeleteCharacter(CharacterItem characterItem) =>
        Delete(characterItem);
    

    public async Task<CharacterItem?> GetCharacterItemAsync(int characterId, int itemId, bool trackChanges) =>
        await FindByCondition(x => x.CharacterId.Equals(characterId) && x.ItemId.Equals(itemId), trackChanges)
        .SingleOrDefaultAsync();

    public async Task<IEnumerable<CharacterItem>> GetCharacterItemsAsync(int characterId, bool trackChanges) =>
        await FindByCondition(x => x.CharacterId.Equals(characterId), trackChanges)
        .ToListAsync();
}
