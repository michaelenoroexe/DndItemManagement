using Shared.DataTransferObjects.ItemCategory;

namespace Service.Contracts;

public interface IItemCategoryService
{
    Task<IEnumerable<ItemCategoryDto>> GetItemCategories(bool trackChanges);
    Task<ItemCategoryDto> CreateItemCategoryAsync(
        ItemCategoryForCreationDto itemCategForCreation, bool trackChanges);
    Task DeleteItemCategoryAsync(int id, bool trackChanges);
    Task UpdateItemCategoryAsync(int id,
        ItemCategoryForUpdateDto itemCategForUpdate, bool trackChanges);
}
