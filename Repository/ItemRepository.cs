using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(RepositoryContext repositoryContext)
    : base(repositoryContext) { }

    public async Task<IEnumerable<Item>> GetAllItemsAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToListAsync();

    public async Task<Item?> GetItemAsync(int itemId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(itemId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateItem(Item item) => Create(item);

    public async Task<IEnumerable<Item>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteItem(Item item) => Delete(item);

    public async Task<IEnumerable<Item>> GetAllItemsForCategoryAsync
        (int categoryId, bool trackChanges) =>
        await FindByCondition(i => i.ItemCategoryId.Equals(categoryId), trackChanges)
        .ToListAsync();

    public async Task<IEnumerable<Item>> GetAllItemsForDmAsync(int dmId, bool trackChanges) =>
        await FindByCondition(i => i.ItemCategoryId.Equals(dmId), trackChanges)
        .ToListAsync();
}
