namespace Repository.Contracts;

public interface IRepositoryManager
{
    IActionRepository Action { get; }
    ICharacterRepository Character { get; }
    IDMRepository DM { get; }
    IItemCategoryRepository ItemCategory { get; }
    IItemRepository Item { get; }
    IRoomRepository Room { get; }
    ICharacterItemRepository CharacterItem { get; }
    Task SaveAsync();
}
