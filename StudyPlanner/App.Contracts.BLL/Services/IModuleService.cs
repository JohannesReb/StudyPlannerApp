using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IModuleService : IEntityService<App.BLL.DTO.Entities.Module>, IModuleRepositoryCustom<App.BLL.DTO.Entities.Module>
{
    
}