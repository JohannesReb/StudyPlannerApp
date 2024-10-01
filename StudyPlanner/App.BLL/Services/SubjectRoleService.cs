using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class SubjectRoleService : BaseEntityService<App.DAL.DTO.ManyToMany.SubjectRole, App.BLL.DTO.ManyToMany.SubjectRole, ISubjectRoleRepository>, ISubjectRoleService
{
    public SubjectRoleService(IAppUnitOfWork uoW, ISubjectRoleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.SubjectRole, App.BLL.DTO.ManyToMany.SubjectRole>(mapper))
    {
    }
    public async Task<List<App.BLL.DTO.ManyToMany.SubjectRole>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllOfSubjectAsync(subjectId, userId)).Select(e => Mapper.Map(e)).ToList();
    }
}