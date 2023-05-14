using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class DMRepository : RepositoryBase<DM>, IDMRepository
{
    public DMRepository(RepositoryContext repositoryContext)
    : base(repositoryContext) { }

    public async Task<IEnumerable<DM>> GetAllDMsAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Login)
    .ToListAsync();

    public async Task<DM?> GetDMAsync(int dmId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(dmId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateDM(DM dm) => Create(dm);

    public async Task<IEnumerable<DM>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteDM(DM dm) => Delete(dm);

    public async Task<DM?> GetDmByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(x => x.Login.Equals(name), trackChanges)
        .FirstOrDefaultAsync();
}
