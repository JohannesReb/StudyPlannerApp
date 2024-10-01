using App.BLL.DTO;
using App.BLL.DTO.Entities;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class CurriculumService : BaseEntityService<App.DAL.DTO.Entities.Curriculum, App.BLL.DTO.Entities.Curriculum, ICurriculumRepository>, ICurriculumService
{
    public CurriculumService(IAppUnitOfWork uoW, ICurriculumRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Entities.Curriculum, App.BLL.DTO.Entities.Curriculum>(mapper))
    {
    }
    public async Task<List<App.BLL.DTO.Entities.Curriculum>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e)).ToList();
    }
}