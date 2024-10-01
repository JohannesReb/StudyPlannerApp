using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class UserEwentService : BaseEntityService<App.DAL.DTO.ManyToMany.UserEwent, App.BLL.DTO.ManyToMany.UserEwent, IUserEwentRepository>, IUserEwentService
{
    public UserEwentService(IAppUnitOfWork uoW, IUserEwentRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.UserEwent, App.BLL.DTO.ManyToMany.UserEwent>(mapper))
    {
    }
}