using Administration.Repository.Contracts;

namespace Administration.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext repositoryContext;
    private readonly Lazy<IDMRepository> dMRepository;
    private readonly Lazy<IRoomRepository> roomRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
        this.dMRepository = new Lazy<IDMRepository>(() => new DMRepository(repositoryContext));
        this.roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
    }

    public IDMRepository DM => dMRepository.Value;
    public IRoomRepository Room => roomRepository.Value;

    public async Task SaveAsync() => await repositoryContext.SaveChangesAsync();
}
