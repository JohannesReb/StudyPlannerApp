using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EwentRoleRepository : BaseEntityRepository<Domain.ManyToMany.EwentRole, App.DAL.DTO.ManyToMany.EwentRole, AppDbContext>, IEwentRoleRepository
{
    public EwentRoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.EwentRole, DTO.ManyToMany.EwentRole>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.EwentRole> CreateQuery(Guid userId = default, bool noTracking = true)
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