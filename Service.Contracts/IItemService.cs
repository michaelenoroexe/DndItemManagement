using Entities.Models;
using Shared.DataTransferObjects.Item;

namespace Service.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync(bool trackChanges);
    Task<IEnumerable<ItemDto>> GetAllItemsForCategoryAsync(int categoryId, bool trackChanges);
    Task<ItemDto> CreateItemAsync(int dmId,
        ItemForCreationDto itemForCreation, bool trackChanges);
    Task DeleteItemAsync(int dmId, int id, bool trackChanges);
    Task UpdateItemAsync(int dmId, int id,
        ItemForUpdateDto itemForUpdate, bool dmTrackChanges, bool itemTrackChanges);
    Task<(ItemForUpdateDto itemToPatch, Item itemEntity)> GetItemForPatchAsync(
        int dmId, int id, bool compTrackChanges, bool empTrackChanges);
    Task SaveChangesForPatchAsync(ItemForUpdateDto itemToPatch, Item itemEntity);
}
