using AutoMapper;
using Entities.Exceptions.ItemCategory;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.ItemCategory;

namespace Service;

internal sealed class ItemCategoryService : IItemCategoryService
{
    private readonly IRepositoryManager repository;
    private readonly IMapper mapper;

    private async Task<ItemCategory> GetItemCategoryAndCheckIfItExists(int itemCategoryId, bool trackChanges)
    {
        var itemCategory = await repository.ItemCategory.GetItemCategoryAsync(itemCategoryId, trackChanges);
        if (itemCategory is null) throw new ItemCategoryNotFoundException(itemCategoryId);

        return itemCategory;
    }

    public ItemCategoryService(IRepositoryManager repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ItemCategoryDto> CreateItemCategoryAsync(ItemCategoryForCreationDto itemCategForCreation, bool trackChanges)
    {
        var itemCategory = mapper.Map<ItemCategory>(itemCategForCreation);

        repository.ItemCategory.CreateItemCategory(itemCategory);
        await repository.SaveAsync();

        var itemCategoryToReturn = mapper.Map<ItemCategoryDto>(itemCategory);

        return itemCategoryToReturn;
    }

    public async Task DeleteItemCategoryAsync(int id, bool trackChanges)
    {
        var itemCategoryDb = await GetItemCategoryAndCheckIfItExists(id, trackChanges);

        repository.ItemCategory.DeleteItemCategory(itemCategoryDb);
        await repository.SaveAsync();
    }

    public async Task<IEnumerable<ItemCategoryDto>> GetItemCategories(bool trackChanges)
    {
        var itemCategoryDb = await repository.ItemCategory.GetAllItemCategoriesAsync(trackChanges);

        var itemCategoryToReturn = mapper.Map<IEnumerable<ItemCategoryDto>>(itemCategoryDb);

        return itemCategoryToReturn;
    }

    public async Task UpdateItemCategoryAsync(int id, ItemCategoryForUpdateDto itemCategForUpdate, bool trackChanges)
    {
        var itemCategoryDb = await GetItemCategoryAndCheckIfItExists(id, trackChanges);

        mapper.Map(itemCategForUpdate, itemCategoryDb);
        await repository.SaveAsync();
    }
}
