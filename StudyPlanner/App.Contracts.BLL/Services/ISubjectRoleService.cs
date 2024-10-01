using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISubjectRoleService : IEntityService<App.BLL.DTO.ManyToMany.SubjectRole>, ISubjectRoleRepositoryCustom<App.BLL.DTO.ManyToMany.SubjectRole>
{
    
}