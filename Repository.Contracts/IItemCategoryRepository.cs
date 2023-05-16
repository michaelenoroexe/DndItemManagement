using Entities.Models;

namespace Repository.Contracts;

public interface IItemCategoryRepository
{
    Task<IEnumerable<ItemCategory>> GetAllItemCategoriesAsync(bool trackChanges);
    Task<ItemCategory?> GetItemCategoryAsync(int itemCategoriesId, bool trackChanges);
    void CreateItemCategory(ItemCategory itemCategory);
    Task<IEnumerable<ItemCategory>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteItemCategory(ItemCategory itemCategory);
}
