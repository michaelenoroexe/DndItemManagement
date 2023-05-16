using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class ItemCategoryRepository : RepositoryBase<ItemCategory>, IItemCategoryRepository
{
    public ItemCategoryRepository(RepositoryContext repositoryContext)
: base(repositoryContext) { }

    public async Task<IEnumerable<ItemCategory>> GetAllItemCategoriesAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToListAsync();

    public async Task<ItemCategory?> GetItemCategoryAsync(int itemCategoryId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(itemCategoryId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateItemCategory(ItemCategory itemCategory) => Create(itemCategory);

    public async Task<IEnumerable<ItemCategory>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteItemCategory(ItemCategory itemCategory) => Delete(itemCategory);
}
