namespace Repository.Contracts;

public interface IActionRepository
{
    Task<IEnumerable<Entities.Models.Action>> GetAllActionsAsync(bool trackChanges);
    Task<Entities.Models.Action?> GetActionAsync(int actionId, bool trackChanges);
    void CreateAction(Entities.Models.Action action);
    Task<IEnumerable<Entities.Models.Action>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    void DeleteAction(Entities.Models.Action action);
}
