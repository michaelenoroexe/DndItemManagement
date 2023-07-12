namespace Repository.Contracts;

public interface IRepositoryManager
{
    IActionRepository Action { get; }
    ICharacterRepository Character { get; }
    IItemCategoryRepository ItemCategory { get; }
    IItemRepository Item { get; }
    ICharacterItemRepository CharacterItem { get; }
    Task SaveAsync();
}
