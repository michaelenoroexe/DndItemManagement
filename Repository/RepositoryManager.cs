using Repository.Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext repositoryContext;
    private readonly Lazy<IActionRepository> actionRepository;
    private readonly Lazy<ICharacterRepository> characterRepository;
    private readonly Lazy<IDMRepository> dMRepository;
    private readonly Lazy<IItemCategoryRepository> itemCategoryRepository;
    private readonly Lazy<IItemRepository> itemRepository;
    private readonly Lazy<IRoomRepository> roomRepository;
    private readonly Lazy<ICharacterItemRepository> characterItemRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
        this.actionRepository = new Lazy<IActionRepository>(() => new ActionRepository(repositoryContext));
        this.characterRepository = new Lazy<ICharacterRepository>(() => new CharacterRepository(repositoryContext));
        this.dMRepository = new Lazy<IDMRepository>(() => new DMRepository(repositoryContext));
        this.itemCategoryRepository = new Lazy<IItemCategoryRepository>(() => new ItemCategoryRepository(repositoryContext));
        this.itemRepository = new Lazy<IItemRepository>(() => new ItemRepository(repositoryContext));
        this.roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
        this.characterItemRepository = new Lazy<ICharacterItemRepository>(() => new CharacterItemRepository(repositoryContext));
    }

    public IActionRepository Action => actionRepository.Value;
    public ICharacterRepository Character => characterRepository.Value;
    public IDMRepository DM => dMRepository.Value;
    public IItemCategoryRepository ItemCategory => itemCategoryRepository.Value;
    public IItemRepository Item => itemRepository.Value;
    public IRoomRepository Room => roomRepository.Value;
    public ICharacterItemRepository CharacterItem => characterItemRepository.Value;

    public async Task SaveAsync() => await repositoryContext.SaveChangesAsync();
}
