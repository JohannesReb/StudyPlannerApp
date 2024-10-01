using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class UserFieldService : BaseEntityService<App.DAL.DTO.ManyToMany.UserField, App.BLL.DTO.ManyToMany.UserField, IUserFieldRepository>, IUserFieldService
{
    public UserFieldService(IAppUnitOfWork uoW, IUserFieldRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.UserField, App.BLL.DTO.ManyToMany.UserField>(mapper))
    {
    }
}