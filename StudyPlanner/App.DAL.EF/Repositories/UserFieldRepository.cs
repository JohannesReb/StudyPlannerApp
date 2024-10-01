using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserFieldRepository : BaseEntityRepository<Domain.ManyToMany.UserField, App.DAL.DTO.ManyToMany.UserField, AppDbContext>, IUserFieldRepository
{
    public UserFieldRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.UserField, DTO.ManyToMany.UserField>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.UserField> CreateQuery(Guid userId = default, bool noTracking = true)
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