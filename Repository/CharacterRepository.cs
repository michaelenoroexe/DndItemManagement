using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class CharacterRepository : RepositoryBase<Character>, ICharacterRepository
{
    public CharacterRepository(RepositoryContext repositoryContext)
    : base(repositoryContext) { }

    public async Task<IEnumerable<Character>> GetAllCharactersAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToListAsync();

    public async Task<Character?> GetCharacterAsync(int characterId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(characterId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateCharacter(Character character) => Create(character);

    public async Task<IEnumerable<Character>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteCharacter(Character character) => Delete(character);
}
