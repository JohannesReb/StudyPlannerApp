using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkTaskRoleRepository : BaseEntityRepository<Domain.ManyToMany.WorkTaskRole, App.DAL.DTO.ManyToMany.WorkTaskRole, AppDbContext>, IWorkTaskRoleRepository
{
    public WorkTaskRoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.WorkTaskRole, DTO.ManyToMany.WorkTaskRole>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.WorkTaskRole> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var roles = RepoDbContext.UserRoles.Where(u => u.UserId.Equals(userId)).Select(u => u.RoleId);
        var query = RepoDbSet.AsQueryable();
        query = query.Where(w => roles.Contains(w.RoleId))
            .Include(w => w.Role);

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    public async Task<List<App.DAL.DTO.ManyToMany.WorkTaskRole>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking)
            .Where(u => u.WorkTaskId.Equals(workTaskId))
            .Select(de => Mapper.Map(de)).ToListAsync();
    }
}