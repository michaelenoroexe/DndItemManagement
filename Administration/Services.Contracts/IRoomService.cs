using Administration.Entities.Models;
using Administration.Shared.DataTransferObjects.Room;

namespace Administration.Service.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomWithDMDto>> GetAllRooms(bool trackChanges);
    Task<IEnumerable<RoomDto>> GetRoomsForDM(int dmId, bool trackChanges);
    Task<RoomDto> GetRoomAsync(int roomId, bool trackChanges);
    Task<Room> GetFullRoomAsync(int id, bool trackChanges);
    Task<RoomDto> CreateRoomForDMAsync(int dmId,
        RoomForCreationDto roomForCreation, bool trackChanges);
    Task DeleteRoomAsync(int dmId, int id, bool trackChanges);
    Task<RoomDto> UpdateRoomAsync(int dmId, int id,
        RoomForUpdateDto roomForUpdate, bool dmTrackChanges, bool roomTrackChanges);
    Task<RoomDto> PartialUpdateRoomAsync(int dmId, int id,
        RoomForUpdateDto roomForUpdate, bool dmTrackChanges, bool roomTrackChanges);
}
