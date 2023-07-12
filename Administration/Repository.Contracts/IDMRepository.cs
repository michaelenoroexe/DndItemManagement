using Administration.Entities.Models;

namespace Administration.Repository.Contracts;

public interface IDMRepository
{
    Task<IEnumerable<DM>> GetAllDMsAsync(bool trackChanges);
    Task<DM?> GetDMAsync(int dmId, bool trackChanges);
    Task<DM?> GetDmByNameAsync(string name, bool trackChanges);
    void CreateDM(DM dm);
    Task<IEnumerable<DM>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteDM(DM dm);
}
