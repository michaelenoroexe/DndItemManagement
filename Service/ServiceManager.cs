using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICharacterService> characterService;
    private readonly Lazy<IItemCategoryService> itemCategoryService;
    private readonly Lazy<IItemService> itemService;
    private readonly Lazy<ICharacterItemsService> characterItemsService;

    public ServiceManager(IConfiguration configuration, IRepositoryManager repository, IMapper mapper)
    {
        characterService = new Lazy<ICharacterService>(() => new CharacterService(repository, mapper));
        itemCategoryService = new Lazy<IItemCategoryService>(() => new ItemCategoryService(repository, mapper));
        itemService = new Lazy<IItemService>(() => new ItemService(repository, mapper));
        characterItemsService = new Lazy<ICharacterItemsService>(() => new CharacterItemsService(repository, mapper));
    }

    public ICharacterItemsService CharacterItemsService => characterItemsService.Value;

    public ICharacterService CharacterService => characterService.Value;

    public IItemCategoryService ItemCategoryService => itemCategoryService.Value;

    public IItemService ItemService => itemService.Value;
}
