namespace Service.Contracts;

public interface IServiceManager
{
    IAuthenticationService AuthenticationService { get; }
    ICharacterItemsService CharacterItemsService { get; }
    ICharacterService CharacterService { get; }
    IDMService DMService { get; }
    IItemCategoryService ItemCategoryService { get; }
    IItemService ItemService { get; }
    IRoomService RoomService { get; }
}