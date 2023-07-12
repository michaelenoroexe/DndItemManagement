namespace Service.Contracts;

public interface IServiceManager
{
    ICharacterItemsService CharacterItemsService { get; }
    ICharacterService CharacterService { get; }
    IItemCategoryService ItemCategoryService { get; }
    IItemService ItemService { get; }
}