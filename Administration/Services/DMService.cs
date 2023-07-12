using AutoMapper;
using Administration.Entities.Exceptions.DM;
using Administration.Entities.Models;
using Administration.Repository.Contracts;
using Administration.Service.Contracts;
using Administration.Shared.DataTransferObjects.DM;

namespace Administration.Service;

internal sealed class DMService : IDMService
{
    private readonly IRepositoryManager repository;
    private readonly IHasher hasher;
    private readonly IMapper mapper;

    private async Task<DM> GetDMAndCheckIfItExists(int id, bool trackChanges)
    {
        var dm = await repository.DM.GetDMAsync(id, trackChanges);
        if (dm is null)
            throw new DMNotFoundException(id);

        return dm;
    }

    public DMService(IRepositoryManager repositoryManager, 
        IHasher hasher, IMapper mapper)
    {
        this.repository = repositoryManager;
        this.hasher = hasher;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<DMDto>> GetAllDMs(bool trackChanges)
    {
        var dms = await repository.DM.GetAllDMsAsync(trackChanges);

        var dmsToReturn = mapper.Map<IEnumerable<DMDto>>(dms);

        return dmsToReturn;
    }

    public async Task<DMDto> GetDMAsync(int id, bool trackChanges)
    {
        var dmDb = await repository.DM.GetDMAsync(id, trackChanges);

        var dm = mapper.Map<DMDto>(dmDb);
        return dm;
    }
    public async Task<DMDto> GetDMAsync(string login, bool trackChanges)
    {
        var dmDb = await repository.DM.GetDmByNameAsync(login, trackChanges);

        var dm = mapper.Map<DMDto>(dmDb);
        return dm;
    }

    public async Task<DMDto> RegisterDMAsync(DMForRegistrationDto dm)
    {
        var dmEntity = mapper.Map<DM>(dm);
        dmEntity.Password = hasher.HashPassword(dmEntity.Password);

        repository.DM.CreateDM(dmEntity);
        await repository.SaveAsync();

        var dmToReturn = mapper.Map<DMDto>(dmEntity);

        return dmToReturn;
    }

    public async Task UpdateDMAsync(int id, DMForUpdateDto dmForUpdate, bool trackChanges)
    {
        var dmDb = await GetDMAndCheckIfItExists(id, trackChanges);
        dmDb.Password = hasher.HashPassword(dmForUpdate.Password!);

        mapper.Map(dmForUpdate, dmDb);
        await repository.SaveAsync();
    }

    public async Task DeleteDMAsync(int dmId)
    {
        var dm = await GetDMAndCheckIfItExists(dmId, true);

        repository.DM.DeleteDM(dm);
        await repository.SaveAsync();
    }

    public async Task PartialUpdateDMAsync(int id, DMForUpdateDto dmForUpdate, bool trackChanges)
    {
        var dmDb = await GetDMAndCheckIfItExists(id, trackChanges);

        if (dmForUpdate.Login is not null) dmDb.Login = dmForUpdate.Login;
        if (dmForUpdate.Password is not null) dmDb.Password = hasher.HashPassword(dmForUpdate.Password);

        mapper.Map(dmForUpdate, dmDb);
        await repository.SaveAsync(); 
    }
}
