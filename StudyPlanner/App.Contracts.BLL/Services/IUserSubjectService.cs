using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserSubjectService : IEntityService<App.BLL.DTO.ManyToMany.UserSubject>, IUserSubjectRepositoryCustom<App.BLL.DTO.ManyToMany.UserSubject>
{
    
}