using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class EwentService : BaseEntityService<App.DAL.DTO.Entities.Ewent, App.BLL.DTO.Entities.Ewent, IEwentRepository>, IEwentService
{
    public EwentService(IAppUnitOfWork uoW, IEwentRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.Ewent, App.BLL.DTO.Entities.Ewent>(mapper))
    {
    }
}