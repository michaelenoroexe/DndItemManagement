﻿using Entities.Models;
using Shared.DataTransferObjects.Room;

namespace Service.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomWithDMDto>> GetAllRooms(bool trackChanges);
    Task<IEnumerable<RoomDto>> GetRoomsForDM(int dmId, bool trackChanges);
    Task<Room> GetFullRoomAsync(int id, bool trackChanges);
    Task<RoomDto> CreateRoomForDMAsync(int dmId,
        RoomForCreationDto roomForCreation, bool trackChanges);
    Task DeleteRoomAsync(int dmId, int id, bool trackChanges);
    Task UpdateRoomAsync(int dmId, int id,
        RoomForUpdateDto roomForUpdate, bool dmTrackChanges, bool roomTrackChanges);
    Task<(RoomForUpdateDto roomToPatch, Room roomEntity)> GetRoomForPatchAsync(
        int dmId, int id, bool dmTrackChanges, bool roomTrackChanges);
    Task SaveChangesForPatchAsync(RoomForUpdateDto roomToPatch, Room roomEntity);
}
