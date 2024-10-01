using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITimeWindowService : IEntityService<App.BLL.DTO.Entities.TimeWindow>, ITimeWindowRepositoryCustom<App.BLL.DTO.Entities.TimeWindow>
{
    
}