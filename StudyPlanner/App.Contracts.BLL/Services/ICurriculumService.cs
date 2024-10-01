using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICurriculumService : IEntityService<App.BLL.DTO.Entities.Curriculum>, ICurriculumRepositoryCustom<App.BLL.DTO.Entities.Curriculum>
{
    
}