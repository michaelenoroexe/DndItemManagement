using AutoMapper;
using Entities.Exceptions.DM;
using Entities.Exceptions.Room;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.Room;

namespace Service;

internal sealed class RoomService : IRoomService
{
    private readonly IRepositoryManager repository;
    private readonly IHasher hasher;
    private readonly IMapper mapper;

    private async Task CheckIfDmExists(int dmId, bool trackChanges)
    {
        var dm = await repository.DM.GetDMAsync(dmId, trackChanges);
        if (dm is null) throw new DMNotFoundException(dmId); 
    }
    private async Task<Room> GetRoomAndCheckIfItExists(int roomId, bool trackChanges)
    {
        var room = await repository.Room.GetRoomAsync(roomId, trackChanges);
        if (room is null) throw new RoomNotFoundException(roomId);

        return room;
    }

    public RoomService(IRepositoryManager repository, IHasher hasher, IMapper mapper)
    {
        this.repository = repository;
        this.hasher = hasher;
        this.mapper = mapper;
    }

    public async Task<RoomDto> CreateRoomForDMAsync(int dmId, RoomForCreationDto roomForCreation, bool trackChanges)
    {
        await CheckIfDmExists(dmId, trackChanges);

        var roomEntity = mapper.Map<Room>(roomForCreation);
        roomEntity.Password = hasher.HashPassword(roomEntity.Password);

        repository.Room.CreateRoom(dmId, roomEntity);
        await repository.SaveAsync();

        var roomToReturn = mapper.Map<RoomDto>(roomEntity);

        return roomToReturn;
    }

    public async Task DeleteRoomAsync(int dmId, int id, bool trackChanges)
    {
        await CheckIfDmExists(dmId, trackChanges);

        var roomDb = await GetRoomAndCheckIfItExists(id, trackChanges);

        repository.Room.DeleteRoom(roomDb);
        await repository.SaveAsync();
    }
    public async Task<RoomDto> GetRoomAsync(int roomId, bool trackChanges)
    {
        var room = await repository.Room.GetRoomAsync(roomId, false);

        var roomToReturn = mapper.Map<RoomDto>(room);

        return roomToReturn;
    }
    public async Task<IEnumerable<RoomWithDMDto>> GetAllRooms(bool trackChanges)
    {
        var dbRooms = await repository.Room.GetAllRoomsWithDmsAsync(trackChanges);

        var roomsToReturn = mapper.Map<IEnumerable<RoomWithDMDto>>(dbRooms);

        return roomsToReturn;
    }
    public async Task<IEnumerable<RoomDto>> GetRoomsForDM(int dmId, bool trackChanges)
    {
        await CheckIfDmExists(dmId, trackChanges);

        var roomsDb = await repository.Room.GetRoomsForDmAsync(dmId, trackChanges);

        var roomsToReturn = mapper.Map<IEnumerable<RoomDto>>(roomsDb);

        return roomsToReturn;
    }

    public async Task<RoomDto> UpdateRoomAsync
        (int dmId, int id, RoomForUpdateDto roomForUpdate, 
        bool dmTrackChanges, bool roomTrackChanges)
    {
        await CheckIfDmExists(dmId, dmTrackChanges);

        var roomDb = await GetRoomAndCheckIfItExists(id, roomTrackChanges);

        mapper.Map(roomForUpdate, roomDb);
        await repository.SaveAsync();

        return mapper.Map<RoomDto>(roomDb);
    }
    public async Task<Room> GetFullRoomAsync(int id, bool trackChanges) =>
        await GetRoomAndCheckIfItExists(id, trackChanges);

    public async Task<RoomDto> PartialUpdateRoomAsync(int dmId, int id, 
        RoomForUpdateDto roomForUpdate, bool dmTrackChanges, bool roomTrackChanges)
    {
        await CheckIfDmExists(dmId, dmTrackChanges);

        var roomDb = await GetRoomAndCheckIfItExists(id, roomTrackChanges);

        if (roomForUpdate.Name is not null) roomDb.Name = roomForUpdate.Name;
        if (roomForUpdate.Password is not null) roomDb.Password = roomForUpdate.Password;
        if (roomForUpdate.Started.HasValue) roomDb.Started = roomForUpdate.Started.Value;
        await repository.SaveAsync();

        return mapper.Map<RoomDto>(roomDb);
    }
}
