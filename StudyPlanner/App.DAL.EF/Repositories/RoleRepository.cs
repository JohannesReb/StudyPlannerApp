using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RoleRepository : BaseEntityRepository<Domain.Identity.Role, App.DAL.DTO.Identity.Role, AppDbContext>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.Identity.Role, DTO.Identity.Role>(mapper))
    {
    }
    protected override IQueryable<Domain.Identity.Role> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var roles = RepoDbContext.UserRoles.AsQueryable().Where(r => r.UserId == userId).Select(r => r.RoleId);
        
        var query = RepoDbSet.AsQueryable();
        query = query.Where(r => roles.Contains(r.Id));

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}