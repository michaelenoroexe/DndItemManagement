using Entities.Models;

namespace Repository.Contracts;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllItemsAsync(bool trackChanges);
    Task<Item?> GetItemAsync(int itemId, bool trackChanges);
    void CreateItem(Item item);
    Task<IEnumerable<Item>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteItem(Item item);
}
