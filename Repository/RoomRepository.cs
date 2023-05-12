using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
{
    public RoomRepository(RepositoryContext repositoryContext)
    : base(repositoryContext) { }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToListAsync();

    public async Task<Room?> GetRoomAsync(int roomId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(roomId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateRoom(Room room) => Create(room);

    public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteRoom(Room room) => Delete(room);
}
