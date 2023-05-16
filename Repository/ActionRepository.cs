using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

internal sealed class ActionRepository : RepositoryBase<Entities.Models.Action>, IActionRepository
{
    public ActionRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext) { }

    public async Task<IEnumerable<Entities.Models.Action>> GetAllActionsAsync(bool trackChanges) =>
    await FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToListAsync();

    public async Task<Entities.Models.Action?> GetActionAsync(int actionId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(actionId), trackChanges)
    .SingleOrDefaultAsync();

    public void CreateAction(Entities.Models.Action action) => Create(action);

    public async Task<IEnumerable<Entities.Models.Action>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public void DeleteAction(Entities.Models.Action action) => Delete(action);
}
