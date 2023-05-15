using Entities.Models;
using Shared.DataTransferObjects.DM;

namespace Service.Contracts;

public interface IDMService
{
    Task<IEnumerable<DMDto>> GetAllDMs(bool trackChanges);
    Task<DMDto> GetDMAsync(int id, bool trackChanges);
    Task<DMDto> RegisterDMAsync(DMForRegistrationDto dm);
    Task UpdateDMAsync(int id, DMForUpdateDto dmForUpdate, bool trackChanges);
    Task<(DMForUpdateDto dmToPatch, DM dmEntity)> GetDMForPatchAsync(int id, bool dmTrackChanges);
    Task SaveChangesForPatchAsync(DMForUpdateDto dmToPatch, DM dmEntity);
}
