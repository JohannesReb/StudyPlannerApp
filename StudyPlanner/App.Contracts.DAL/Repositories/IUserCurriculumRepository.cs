using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUserCurriculumRepository : IEntityRepository<App.DAL.DTO.ManyToMany.UserCurriculum>
{
    
}