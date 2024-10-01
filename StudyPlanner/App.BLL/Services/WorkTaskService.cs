using App.BLL.DTO.Entities;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.BLL;
using Microsoft.AspNetCore.Identity;

namespace App.BLL.Services;

public class WorkTaskService : BaseEntityService<App.DAL.DTO.Entities.WorkTask, App.BLL.DTO.Entities.WorkTask, IWorkTaskRepository>, IWorkTaskService
{
    private readonly IAppUnitOfWork _uow;
    public WorkTaskService(IAppUnitOfWork uoW, IWorkTaskRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.WorkTask, App.BLL.DTO.Entities.WorkTask>(mapper))
    {
        _uow = uoW;
    }

    public async Task<List<App.BLL.DTO.Entities.WorkTask>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.WorkTask>> GetAllUnPlannedAsync(Guid userId)
    {
        return (await Repository.GetAllUnPlannedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        return (await Repository.GetAllSortedOfSubjectAsync(userId, subjectId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.WorkTask>> GetAllPublicSortedAsync(Guid userId)
    {
        return (await Repository.GetAllPublicSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllPublicSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        return (await Repository.GetAllPublicSortedOfSubjectAsync(userId, subjectId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.WorkTask>> GetAllChosenSortedAsync(Guid userId)
    {
        return (await Repository.GetAllChosenSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllChosenSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        return (await Repository.GetAllChosenSortedOfSubjectAsync(userId, subjectId)).Select(e => Mapper.Map(e)).ToList();
    }
}