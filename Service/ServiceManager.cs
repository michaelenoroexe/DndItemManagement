using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    //private readonly Lazy<IActionService> actionRepository;
    private readonly Lazy<IAuthenticationService> authenticationService;
    private readonly Lazy<ICharacterService> characterService;
    private readonly Lazy<IDMService> dMService;
    private readonly Lazy<IItemCategoryService> itemCategoryService;
    //private readonly Lazy<IItemService> itemService;
    private readonly Lazy<IRoomService> roomService;
    private readonly Lazy<ICharacterItemsService> characterItemsService;

    public ServiceManager(IConfiguration configuration, IRepositoryManager repository, IHasher hasher, IMapper mapper)
    {
        authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(configuration, repository, hasher));
        characterService = new Lazy<ICharacterService>(() => new CharacterService(repository, mapper));
        dMService = new Lazy<IDMService>(() => new DMService(repository, hasher, mapper));
        itemCategoryService = new Lazy<IItemCategoryService>(() => new ItemCategoryService(repository, mapper));
        //this.itemService = new Lazy<IItemService>(() => new ItemService(repository, mapper));
        roomService = new Lazy<IRoomService>(() => new RoomService(repository, hasher, mapper));
        characterItemsService = new Lazy<ICharacterItemsService>(() => new CharacterItemsService(repository, mapper));
    }

    public IAuthenticationService AuthenticationService => authenticationService.Value;

    public ICharacterItemsService CharacterItemsService => characterItemsService.Value;

    public ICharacterService CharacterService => characterService.Value;

    public IDMService DMService => dMService.Value;

    public IItemCategoryService ItemCategoryService => itemCategoryService.Value;

    public IItemService ItemService => throw new NotImplementedException();

    public IRoomService RoomService => roomService.Value;
}
