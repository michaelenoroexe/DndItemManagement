using Entities.Models;

namespace Repository.Contracts;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllItemsForCategoryAsync(int categoryId, bool trackChanges);
    Task<IEnumerable<Item>> GetAllItemsForDmAsync(int dmId, bool trackChanges);
    Task<IEnumerable<Item>> GetAllItemsAsync(bool trackChanges);
    Task<Item?> GetItemAsync(int itemId, bool trackChanges);
    void CreateItem(Item item);
    Task<IEnumerable<Item>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteItem(Item item);
}
