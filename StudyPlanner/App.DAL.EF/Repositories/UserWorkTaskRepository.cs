using App.Contracts.DAL.Repositories;
using App.DAL.DTO.ManyToMany;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserWorkTaskRepository : BaseEntityRepository<Domain.ManyToMany.UserWorkTask, App.DAL.DTO.ManyToMany.UserWorkTask, AppDbContext>, IUserWorkTaskRepository
{
    public UserWorkTaskRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.UserWorkTask, DTO.ManyToMany.UserWorkTask>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.UserWorkTask> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query.Where(u => u.UserId.Equals(userId))
            .Include(u => u.WorkTask);
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public async Task<List<App.DAL.DTO.ManyToMany.UserWorkTask>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return Localize(await CreateQuery(userId, noTracking)
            .Where(u => u.WorkTaskId.Equals(workTaskId))
            .Select(de => Mapper.Map(de)).ToListAsync());
    }
    
    public async Task<App.DAL.DTO.ManyToMany.UserWorkTask?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return Localize(Mapper.Map((await CreateQuery(userId, noTracking)
            .Where(u => u.WorkTaskId.Equals(workTaskId))
            .ToListAsync()).FirstOrDefault()));
    }
    
    public override List<UserWorkTask> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<UserWorkTask>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).ToList());
    }

    public override UserWorkTask? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(base.FirstOrDefault(id, userId, noTracking));
    }

    public override async Task<UserWorkTask?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(await base.FirstOrDefaultAsync(id, userId, noTracking));
    }
    
    private static UserWorkTask? Localize(UserWorkTask? userWorkTask)
    {
        if (userWorkTask == default)
        {
            return userWorkTask;
        }
        userWorkTask.CompletedAt = userWorkTask.CompletedAt?.ToLocalTime();
        userWorkTask.WorkTask!.Deadline = userWorkTask.WorkTask!.Deadline?.ToLocalTime();
        return userWorkTask;
    }
    
    private static List<UserWorkTask> Localize(List<UserWorkTask> userWorkTasks)
    {
        foreach (var userWorkTask in userWorkTasks)
        {
            userWorkTask.CompletedAt = userWorkTask.CompletedAt?.ToLocalTime();
            userWorkTask.WorkTask!.Deadline = userWorkTask.WorkTask!.Deadline?.ToLocalTime();
        }
        return userWorkTasks;
    }
}