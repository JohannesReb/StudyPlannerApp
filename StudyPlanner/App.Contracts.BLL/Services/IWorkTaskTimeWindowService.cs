using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IWorkTaskTimeWindowService : IEntityService<App.BLL.DTO.ManyToMany.WorkTaskTimeWindow>, IWorkTaskTimeWindowRepositoryCustom<App.BLL.DTO.ManyToMany.WorkTaskTimeWindow>
{
    
}