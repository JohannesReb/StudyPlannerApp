using App.BLL.DTO.Entities;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IWorkTaskService : IEntityService<App.BLL.DTO.Entities.WorkTask>, IWorkTaskRepositoryCustom<App.BLL.DTO.Entities.WorkTask>
{
}