using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ModuleRepository : BaseEntityRepository<Domain.DbEntities.Module, App.DAL.DTO.Entities.Module, AppDbContext>, IModuleRepository
{
    public ModuleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.Module, App.DAL.DTO.Entities.Module>(mapper))
    {
    }

    public async Task<List<App.DAL.DTO.Entities.Module>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId).OrderBy(m => m.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.DAL.DTO.Entities.Module>> GetAllSortedOfCurriculumAsync(Guid curriculumId, Guid userId = default)
    {
        var query = CreateQuery(userId).Where(m => m.CurriculumId.Equals(curriculumId)).OrderBy(m => m.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }
}