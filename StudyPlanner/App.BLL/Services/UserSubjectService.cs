using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class UserSubjectService : BaseEntityService<App.DAL.DTO.ManyToMany.UserSubject, App.BLL.DTO.ManyToMany.UserSubject, IUserSubjectRepository>, IUserSubjectService
{
    public UserSubjectService(IAppUnitOfWork uoW, IUserSubjectRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.UserSubject, App.BLL.DTO.ManyToMany.UserSubject>(mapper))
    {
    }
    public async Task<List<App.BLL.DTO.ManyToMany.UserSubject>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllOfSubjectAsync(subjectId, userId)).Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<App.BLL.DTO.ManyToMany.UserSubject> GetFirstOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetFirstOfSubjectAsync(subjectId, userId))!;
    }
}