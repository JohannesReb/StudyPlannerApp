using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class SubjectService : BaseEntityService<App.DAL.DTO.Entities.Subject, App.BLL.DTO.Entities.Subject, ISubjectRepository>, ISubjectService
{
    public SubjectService(IAppUnitOfWork uoW, ISubjectRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.Subject, App.BLL.DTO.Entities.Subject>(mapper))
    {
    }

    public async Task<List<App.BLL.DTO.Entities.Subject>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<App.BLL.DTO.Entities.Subject>> GetAllSortedOfModuleAsync(Guid userId, Guid moduleId)
    {
        return (await Repository.GetAllSortedOfModuleAsync(userId, moduleId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.Subject>> GetAllPublicSortedAsync(Guid userId)
    {
        return (await Repository.GetAllPublicSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.Subject>> GetAllPublicSortedOfCurriculumAsync(Guid userId, Guid curriculumId)
    {
        return (await Repository.GetAllPublicSortedOfCurriculumAsync(userId, curriculumId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.Subject>> GetAllChosenSortedAsync(Guid userId)
    {
        return (await Repository.GetAllChosenSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.Subject>> GetAllChosenSortedOfCurriculumAsync(Guid userId, Guid curriculumId)
    {
        return (await Repository.GetAllChosenSortedOfCurriculumAsync(userId, curriculumId)).Select(e => Mapper.Map(e)).ToList();
    }
}