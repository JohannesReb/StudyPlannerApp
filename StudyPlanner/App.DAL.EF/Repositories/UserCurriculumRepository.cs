using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserCurriculumRepository : BaseEntityRepository<Domain.ManyToMany.UserCurriculum, App.DAL.DTO.ManyToMany.UserCurriculum, AppDbContext>, IUserCurriculumRepository
{
    public UserCurriculumRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.UserCurriculum, DTO.ManyToMany.UserCurriculum>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.UserCurriculum> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query;

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}