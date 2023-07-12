namespace Administration.Repository.Contracts;

public interface IRepositoryManager
{
    IDMRepository DM { get; }
    IRoomRepository Room { get; }
    Task SaveAsync();
}
