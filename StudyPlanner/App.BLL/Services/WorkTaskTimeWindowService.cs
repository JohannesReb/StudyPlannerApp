using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class WorkTaskTimeWindowService : BaseEntityService<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow, App.BLL.DTO.ManyToMany.WorkTaskTimeWindow, IWorkTaskTimeWindowRepository>, IWorkTaskTimeWindowService
{
    public WorkTaskTimeWindowService(IAppUnitOfWork uoW, IWorkTaskTimeWindowRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow, App.BLL.DTO.ManyToMany.WorkTaskTimeWindow>(mapper))
    {
    }
    public async Task<App.BLL.DTO.ManyToMany.WorkTaskTimeWindow?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetFirstOfWorkTaskAsync(workTaskId, userId))!;
    }
    public async Task<List<App.BLL.DTO.ManyToMany.WorkTaskTimeWindow>> GetAllSortedAsync(Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(w => Mapper.Map(w)).ToList();
    }
}