using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserEwentRepository : BaseEntityRepository<Domain.ManyToMany.UserEwent, App.DAL.DTO.ManyToMany.UserEwent, AppDbContext>, IUserEwentRepository
{
    public UserEwentRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.UserEwent, DTO.ManyToMany.UserEwent>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.UserEwent> CreateQuery(Guid userId = default, bool noTracking = true)
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