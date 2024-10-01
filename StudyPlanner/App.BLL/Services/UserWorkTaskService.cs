using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class UserWorkTaskService : BaseEntityService<App.DAL.DTO.ManyToMany.UserWorkTask, App.BLL.DTO.ManyToMany.UserWorkTask, IUserWorkTaskRepository>, IUserWorkTaskService
{
    public UserWorkTaskService(IAppUnitOfWork uoW, IUserWorkTaskRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.UserWorkTask, App.BLL.DTO.ManyToMany.UserWorkTask>(mapper))
    {
    }

    public async Task<List<App.BLL.DTO.ManyToMany.UserWorkTask>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllOfWorkTaskAsync(workTaskId, userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<App.BLL.DTO.ManyToMany.UserWorkTask?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetFirstOfWorkTaskAsync(workTaskId, userId))!;
    }
}