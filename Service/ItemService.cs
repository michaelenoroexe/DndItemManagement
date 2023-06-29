using AutoMapper;
using Entities.Exceptions.DM;
using Entities.Exceptions.Item;
using Entities.Exceptions.Room;
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
        private async Task CheckIfRoomExists(int roomId)
        {
            var room = await repository.Room.GetRoomAsync(roomId, false);
            if (room is null) throw new RoomNotFoundException(roomId);
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

        public async Task<ItemDto> CreateItemAsync(int roomId,
            ItemForCreationDto itemForCreation, bool trackChanges)
        {
            await CheckIfRoomExists(roomId);

            var itemEntity = mapper.Map<Item>(itemForCreation);

            repository.Item.CreateItem(itemEntity);
            await repository.SaveAsync();

            var itemToReturn = mapper.Map<ItemDto>(itemEntity);

            return itemToReturn;
        }

        public async Task DeleteItemAsync(int roomId, int id, bool trackChanges)
        {
            await CheckIfRoomExists(roomId);
            var item = await GetItemAndCheckIfItExists(id, trackChanges);

            repository.Item.DeleteItem(item);
            await repository.SaveAsync();
        }

        public async Task<ItemDto> UpdateItemAsync(int roomId, int id,
            ItemForUpdateDto itemForUpdate, bool roomTrackChanges, bool itemTrackChanges)
        {
            await CheckIfRoomExists(roomId);
            var itemDb = await GetItemAndCheckIfItExists(id, itemTrackChanges);

            mapper.Map(itemForUpdate, itemDb);
            await repository.SaveAsync();

            return mapper.Map<ItemDto>(itemDb);
        }
    }
}
