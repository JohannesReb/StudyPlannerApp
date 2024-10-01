using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserWorkTaskService : IEntityService<App.BLL.DTO.ManyToMany.UserWorkTask>, IUserWorkTaskRepositoryCustom<App.BLL.DTO.ManyToMany.UserWorkTask>
{
    
}