using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserSubjectRepository : BaseEntityRepository<Domain.ManyToMany.UserSubject, App.DAL.DTO.ManyToMany.UserSubject, AppDbContext>, IUserSubjectRepository
{
    public UserSubjectRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.UserSubject, DTO.ManyToMany.UserSubject>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.UserSubject> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query.Where(u => u.UserId.Equals(userId)).Include(u => u.Subject);

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    public async Task<List<App.DAL.DTO.ManyToMany.UserSubject>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return (await CreateQuery(userId, noTracking)
            .Where(u => u.SubjectId.Equals(subjectId))
            .Select(de => Mapper.Map(de)).ToListAsync());
    }
    public async Task<App.DAL.DTO.ManyToMany.UserSubject> GetFirstOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true)
    {
        return (await CreateQuery(userId, noTracking)
            .Where(u => u.SubjectId.Equals(subjectId))
            .ToListAsync()).Select(de => Mapper.Map(de)).FirstOrDefault()!;
    }
}