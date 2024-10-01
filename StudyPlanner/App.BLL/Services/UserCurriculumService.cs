using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class UserCurriculumService : BaseEntityService<App.DAL.DTO.ManyToMany.UserCurriculum, App.BLL.DTO.ManyToMany.UserCurriculum, IUserCurriculumRepository>, IUserCurriculumService
{
    public UserCurriculumService(IAppUnitOfWork uoW, IUserCurriculumRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.UserCurriculum, App.BLL.DTO.ManyToMany.UserCurriculum>(mapper))
    {
    }
}