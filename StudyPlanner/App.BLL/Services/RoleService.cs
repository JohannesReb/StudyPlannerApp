using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class RoleService : BaseEntityService<App.DAL.DTO.Identity.Role, App.BLL.DTO.Identity.Role, IRoleRepository>, IRoleService
{
    public RoleService(IAppUnitOfWork uoW, IRoleRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Identity.Role, App.BLL.DTO.Identity.Role>(mapper))
    {
    }
}