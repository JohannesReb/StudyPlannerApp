using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IWorkTaskRoleService : IEntityService<App.BLL.DTO.ManyToMany.WorkTaskRole>, IWorkTaskRoleRepositoryCustom<App.BLL.DTO.ManyToMany.WorkTaskRole>
{
    
}