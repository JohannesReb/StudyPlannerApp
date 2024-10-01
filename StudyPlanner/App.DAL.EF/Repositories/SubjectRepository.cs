using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SubjectRepository : BaseEntityRepository<Domain.DbEntities.Subject, App.DAL.DTO.Entities.Subject, AppDbContext>, ISubjectRepository
{
    public SubjectRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.Subject, DTO.Entities.Subject>(mapper))
    {
    }
    protected override IQueryable<Domain.DbEntities.Subject> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query.Include(s => s.Module);

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public async Task<List<App.DAL.DTO.Entities.Subject>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId).OrderBy(w => w.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.Subject>> GetAllSortedOfModuleAsync(Guid userId, Guid moduleId)
    {
        var query = CreateQuery(userId).Where(w => w.ModuleId == moduleId).OrderBy(w => w.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }
    
    public async Task<List<App.DAL.DTO.Entities.Subject>> GetAllPublicSortedAsync(Guid userId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var query = CreateQuery(userId).Where(w => !subjects.Contains(w.Id)).OrderBy(s => s.Module!.Label).ThenBy(s => s.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.Subject>> GetAllPublicSortedOfCurriculumAsync(Guid userId, Guid curriculumId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var query = CreateQuery(userId)
            .Where(s => s.ModuleId != null && s.Module!.CurriculumId.Equals(curriculumId) && !subjects.Contains(s.Id))
            .OrderBy(s => s.Module!.Label)
            .ThenBy(s => s.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }
    public async Task<List<App.DAL.DTO.Entities.Subject>> GetAllChosenSortedAsync(Guid userId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var query = CreateQuery(userId).Where(s => subjects.Contains(s.Id)).OrderBy(s => s.Module!.Label)
            .ThenBy(s => s.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }

    public async Task<List<DTO.Entities.Subject>> GetAllChosenSortedOfCurriculumAsync(Guid userId, Guid curriculumId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var query = CreateQuery(userId)
            .Where(s => s.ModuleId != null && s.Module!.CurriculumId.Equals(curriculumId) && subjects.Contains(s.Id))
            .OrderBy(s => s.Module!.Label)
            .ThenBy(s => s.Label);
        var res = await query.ToListAsync();
        
        return res.Select(e => Mapper.Map(e)).ToList();
    }
}