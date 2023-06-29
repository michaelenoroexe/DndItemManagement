using Entities.Models;
using Shared.DataTransferObjects.Item;

namespace Service.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync(bool trackChanges);
    Task<IEnumerable<ItemDto>> GetAllItemsForCategoryAsync(int categoryId, bool trackChanges);
    Task<IEnumerable<ItemDto>> GetAllItemsForDmAsync(int dmId, bool trackChanges);
    Task<ItemDto> CreateItemAsync(int roomId,
        ItemForCreationDto itemForCreation, bool trackChanges);
    Task DeleteItemAsync(int roomId, int id, bool trackChanges);
    Task<ItemDto> UpdateItemAsync(int roomId, int id,
        ItemForUpdateDto itemForUpdate, bool roomTrackChanges, bool itemTrackChanges);
}
