using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class WorkTaskRoleService : BaseEntityService<App.DAL.DTO.ManyToMany.WorkTaskRole, App.BLL.DTO.ManyToMany.WorkTaskRole, IWorkTaskRoleRepository>, IWorkTaskRoleService
{
    public WorkTaskRoleService(IAppUnitOfWork uoW, IWorkTaskRoleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.WorkTaskRole, App.BLL.DTO.ManyToMany.WorkTaskRole>(mapper))
    {
    }
    public async Task<List<App.BLL.DTO.ManyToMany.WorkTaskRole>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllOfWorkTaskAsync(workTaskId, userId)).Select(e => Mapper.Map(e)).ToList();
    }
}