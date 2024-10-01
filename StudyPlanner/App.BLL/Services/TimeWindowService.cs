using App.BLL.DTO.Entities;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class TimeWindowService : BaseEntityService<App.DAL.DTO.Entities.TimeWindow, App.BLL.DTO.Entities.TimeWindow, ITimeWindowRepository>, ITimeWindowService
{
    public TimeWindowService(IAppUnitOfWork uoW, ITimeWindowRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.TimeWindow, App.BLL.DTO.Entities.TimeWindow>(mapper))
    {
    }
    public async Task<List<App.BLL.DTO.Entities.TimeWindow>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.TimeWindow>> GetAllActiveSortedAsync(Guid userId)
    {
        return (await Repository.GetAllActiveSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.BLL.DTO.Entities.TimeWindow>> GetAllAvailableAsync(Guid workTaskId, Guid userId)
    {
        return (await Repository.GetAllAvailableAsync(workTaskId, userId)).Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<TimeWindow?> FindByWorkTaskIdAsync(Guid workTaskId, Guid userId)
    {
        return Mapper.Map(await Repository.FindByWorkTaskIdAsync(workTaskId, userId))!;
    }
}