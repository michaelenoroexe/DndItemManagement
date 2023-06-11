using Entities.Models;

namespace Repository.Contracts;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);
    Task<IEnumerable<Room>> GetAllRoomsWithDmsAsync(bool trackChanges);
    Task<IEnumerable<Room>> GetRoomsForDmAsync(int dmId, bool trackChanges);
    Task<Room?> GetRoomAsync(int roomId, bool trackChanges);
    void CreateRoom(int dmId, Room room);
    Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteRoom(Room room);
}
