using AutoMapper;
using Entities.Exceptions.DM;
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

        private async Task CheckIfDmExists(int dmId)
        {
            var dm = await repository.DM.GetDMAsync(dmId, false);
            if (dm is null) throw new DMNotFoundException(dmId);
        }
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
            await CheckIfDmExists(dmId);

            var itemsDb = await repository.Item.GetAllItemsForDmAsync(dmId, trackChanges);

            var itemsToReturn = mapper.Map<IEnumerable<ItemDto>>(itemsDb);

            return itemsToReturn;
        }

        public async Task<ItemDto> CreateItemAsync(int dmId,
            ItemForCreationDto itemForCreation, bool trackChanges)
        {
            await CheckIfDmExists(dmId);

            var itemEntity = mapper.Map<Item>(itemForCreation);

            repository.Item.CreateItem(itemEntity);
            await repository.SaveAsync();

            var itemToReturn = mapper.Map<ItemDto>(itemEntity);

            return itemToReturn;
        }

        public async Task DeleteItemAsync(int dmId, int id, bool trackChanges)
        {
            await CheckIfDmExists(dmId);
            var item = await GetItemAndCheckIfItExists(id, trackChanges);

            repository.Item.DeleteItem(item);
            await repository.SaveAsync();
        }

        public async Task UpdateItemAsync(int dmId, int id,
            ItemForUpdateDto itemForUpdate, bool dmTrackChanges, bool itemTrackChanges)
        {
            await CheckIfDmExists(dmId);
            var itemDb = await GetItemAndCheckIfItExists(id, itemTrackChanges);

            mapper.Map(itemForUpdate, itemDb);
            await repository.SaveAsync();
        }

        public async Task<(ItemForUpdateDto itemToPatch, Item itemEntity)> GetItemForPatchAsync(
            int dmId, int id, bool dmTrackChanges, bool itemTrackChanges)
        {
            await CheckIfDmExists(dmId);
            var itemDb = await GetItemAndCheckIfItExists(id, itemTrackChanges);

            var itemToPatch = mapper.Map<ItemForUpdateDto>(itemDb);

            return (itemToPatch, itemDb);
        }
        public async Task SaveChangesForPatchAsync(ItemForUpdateDto itemToPatch, Item itemEntity)
        {
            mapper.Map(itemToPatch, itemEntity);
            await repository.SaveAsync();
        }
    }
}
