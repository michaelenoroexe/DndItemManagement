using Entities.Models;
using Shared.DataTransferObjects.DM;

namespace Service.Contracts;

public interface IDMService
{
    Task<IEnumerable<DMDto>> GetAllDMs(bool trackChanges);
    Task<DMDto> GetDMAsync(int id, bool trackChanges);
    Task<DMDto> GetDMAsync(string login, bool trackChanges);
    Task<DMDto> RegisterDMAsync(DMForRegistrationDto dm);
    Task DeleteDMAsync(int dmId);
    Task UpdateDMAsync(int id, DMForUpdateDto dmForUpdate, bool trackChanges);
}
