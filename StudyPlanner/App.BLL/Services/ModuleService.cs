using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class ModuleService : BaseEntityService<App.DAL.DTO.Entities.Module, App.BLL.DTO.Entities.Module, IModuleRepository>, IModuleService
{
    public ModuleService(IAppUnitOfWork uoW, IModuleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.Module, App.BLL.DTO.Entities.Module>(mapper))
    {
    }

    public async Task<List<App.BLL.DTO.Entities.Module>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.Module>> GetAllSortedOfCurriculumAsync(Guid userId, Guid curriculumId)
    {
        return (await Repository.GetAllSortedOfCurriculumAsync(userId, curriculumId)).Select(e => Mapper.Map(e)).ToList();
    }
}