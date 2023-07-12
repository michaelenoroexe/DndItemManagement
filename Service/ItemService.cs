using AutoMapper;
using Entities.Exceptions.Item;
using Entities.Models;
using Repository.Contracts;
using Shared.DataTransferObjects.Item;

namespace Service.Contracts
{

    public sealed class ItemService : IItemService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        private async Task<Item> GetItemAndCheckIfItExists(int itemId, bool trackChanges)
        {
            var item = await repository.Item.GetItemAsync(itemId, trackChanges);
            if (item is null) throw new ItemNotFoundException(itemId);

            return item;
        }

        public ItemService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync(bool trackChanges)
        {
            var itemsDb = await repository.Item.GetAllItemsAsync(true);

            var itemsToReturn = mapper.Map<IEnumerable<ItemDto>>(itemsDb);

            return itemsToReturn;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsForCategoryAsync(int categoryId, bool trackChanges)
        {
            var itemsDb = await repository.Item.GetAllItemsForCategoryAsync(categoryId, trackChanges);

            var itemsToReturn = mapper.Map<IEnumerable<ItemDto>>(itemsDb);

            return itemsToReturn;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsForDmAsync
                (int dmId, bool trackChanges)
        {
            var itemsDb = await repository.Item.GetAllItemsForDmAsync(dmId, trackChanges);

            var itemsToReturn = mapper.Map<IEnumerable<ItemDto>>(itemsDb);

            return itemsToReturn;
        }

        public async Task<ItemDto> CreateItemAsync(int roomId,
            ItemForCreationDto itemForCreation, bool trackChanges)
        {
            var itemEntity = mapper.Map<Item>(itemForCreation);

            repository.Item.CreateItem(itemEntity);
            await repository.SaveAsync();

            var itemToReturn = mapper.Map<ItemDto>(itemEntity);

            return itemToReturn;
        }

        public async Task DeleteItemAsync(int roomId, int id, bool trackChanges)
        {
            var item = await GetItemAndCheckIfItExists(id, trackChanges);

            repository.Item.DeleteItem(item);
            await repository.SaveAsync();
        }

        public async Task<ItemDto> UpdateItemAsync(int roomId, int id,
            ItemForUpdateDto itemForUpdate, bool roomTrackChanges, bool itemTrackChanges)
        {
            var itemDb = await GetItemAndCheckIfItExists(id, itemTrackChanges);

            mapper.Map(itemForUpdate, itemDb);
            await repository.SaveAsync();

            return mapper.Map<ItemDto>(itemDb);
        }

        public async Task<ItemDto> PartialUpdateItemAsync(int roomId, int id, 
            ItemForPatchDto itemForUpdate, bool roomTrackChanges, bool itemTrackChanges)
        {
            var itemDb = await GetItemAndCheckIfItExists(id, itemTrackChanges);

            if (itemForUpdate.Name is not  null) itemDb.Name = itemForUpdate.Name;
            if (itemForUpdate.MaxDurability is not  null) itemDb.MaxDurability = itemForUpdate.MaxDurability.Value;
            if (itemForUpdate.Price is not  null) itemDb.Price = itemForUpdate.Price.Value;
            if (itemForUpdate.Weight is not  null) itemDb.Weight = itemForUpdate.Weight.Value;
            if (itemForUpdate.SecretItemDescription is not  null) itemDb.SecretItemDescription = itemForUpdate.SecretItemDescription;
            if (itemForUpdate.ItemDescription is not  null) itemDb.ItemDescription = itemForUpdate.ItemDescription;
            await repository.SaveAsync();

            return mapper.Map<ItemDto>(itemDb);
        }
    }
}
