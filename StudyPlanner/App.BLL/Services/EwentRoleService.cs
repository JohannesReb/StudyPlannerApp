using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class EwentRoleService : BaseEntityService<App.DAL.DTO.ManyToMany.EwentRole, App.BLL.DTO.ManyToMany.EwentRole, IEwentRoleRepository>, IEwentRoleService
{
    public EwentRoleService(IAppUnitOfWork uoW, IEwentRoleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ManyToMany.EwentRole, App.BLL.DTO.ManyToMany.EwentRole>(mapper))
    {
    }
}