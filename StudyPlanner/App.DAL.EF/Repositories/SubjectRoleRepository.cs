using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SubjectRoleRepository : BaseEntityRepository<Domain.ManyToMany.SubjectRole, App.DAL.DTO.ManyToMany.SubjectRole, AppDbContext>, ISubjectRoleRepository
{
    public SubjectRoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.SubjectRole, DTO.ManyToMany.SubjectRole>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.SubjectRole> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query.Include(s => s.Role);

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    public async Task<List<App.DAL.DTO.ManyToMany.SubjectRole>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return (await CreateQuery(userId, noTracking)
            .Where(u => u.SubjectId.Equals(subjectId))
            .Select(de => Mapper.Map(de)).ToListAsync());
    }
}