using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISubjectService : IEntityService<App.BLL.DTO.Entities.Subject>, ISubjectRepositoryCustom<App.BLL.DTO.Entities.Subject>
{
    
}