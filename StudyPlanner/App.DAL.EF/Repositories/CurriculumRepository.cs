using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Entities;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CurriculumRepository : BaseEntityRepository<Domain.DbEntities.Curriculum, App.DAL.DTO.Entities.Curriculum, AppDbContext>, ICurriculumRepository
{
    public CurriculumRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.Curriculum, DTO.Entities.Curriculum>(mapper))
    {
    }
    protected override IQueryable<Domain.DbEntities.Curriculum> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query;

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    public async Task<List<App.DAL.DTO.Entities.Curriculum>> GetAllSortedAsync(Guid userId = default)
    {
        var query = CreateQuery(userId).OrderBy(c => c.Label);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Mapper.Map(e)).ToList().ToList());
    }

    public override List<Curriculum> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<Curriculum>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).ToList());
    }

    public override Curriculum? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(base.FirstOrDefault(id, userId, noTracking));
    }

    public override async Task<Curriculum?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(await base.FirstOrDefaultAsync(id, userId, noTracking));
    }
    private static Curriculum? Localize(Curriculum? curriculum)
    {
        if (curriculum == default)
        {
            return curriculum;
        }
        curriculum.From = curriculum.From.ToLocalTime();
        curriculum.Until = curriculum.Until?.ToLocalTime();
        return curriculum;
    }
    
    private static List<Curriculum> Localize(List<Curriculum> curriculums)
    {
        foreach (var curriculum in curriculums)
        {
            curriculum.From = curriculum.From.ToLocalTime();
            curriculum.Until = curriculum.Until?.ToLocalTime();
        }
        return curriculums;
    }
}