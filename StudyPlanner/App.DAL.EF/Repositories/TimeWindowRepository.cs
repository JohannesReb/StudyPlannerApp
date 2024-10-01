using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Entities;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TimeWindowRepository : BaseEntityRepository<Domain.DbEntities.TimeWindow, App.DAL.DTO.Entities.TimeWindow, AppDbContext>, ITimeWindowRepository
{
    public TimeWindowRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.TimeWindow, DTO.Entities.TimeWindow>(mapper))
    {
    }
    protected override IQueryable<Domain.DbEntities.TimeWindow> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable().Where(t => t.UserId == userId);
        query = query;

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    public async Task<List<App.DAL.DTO.Entities.TimeWindow>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId).OrderBy(w => w.From).ThenBy(w => w.Until);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Mapper.Map(e)).ToList());
    }
    public async Task<List<App.DAL.DTO.Entities.TimeWindow>> GetAllActiveSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId).Where(t => t.Until > DateTime.UtcNow).OrderBy(w => w.From).ThenBy(w => w.Until);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Mapper.Map(e)).ToList());
    }
    
    public async Task<List<App.DAL.DTO.Entities.TimeWindow>> GetAllAvailableAsync(Guid userWorkTaskId, Guid userId)
    {
        var userWorkTask = RepoDbContext.UserWorkTasks.Include(u => u.WorkTask).FirstOrDefault(u => u.Id.Equals(userWorkTaskId));
        var timeSpent = userWorkTask!.TimeSpent ?? TimeSpan.Zero;
        var timeExpectancy = userWorkTask.WorkTask!.TimeExpectancy ?? TimeSpan.Zero;
        var totalTime = TimeSpan.Zero < timeExpectancy - timeSpent ? timeExpectancy - timeSpent : TimeSpan.Zero;
        
        var query = CreateQuery(userId).Where(t => t.FreeTime >= totalTime && t.Until > DateTime.UtcNow)
            .OrderBy(w => w.From).ThenBy(w => w.Until);
        
        return Localize(await query.Select(e => Mapper.Map(e)).ToListAsync());
    }

    public async Task<TimeWindow?> FindByWorkTaskIdAsync(Guid workTaskId, Guid userId)
    {

        var timeWindow = (await RepoDbContext.WorkTaskTimeWindows
            .Include(workTaskTimeWindow => workTaskTimeWindow.TimeWindow).FirstOrDefaultAsync(w => w.WorkTaskId.Equals(workTaskId)))?.TimeWindow;
        return Localize(Mapper.Map(timeWindow)!);
    }

    public override List<TimeWindow> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<TimeWindow>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).ToList());
    }

    public override TimeWindow? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(base.FirstOrDefault(id, userId, noTracking));
    }

    public override async Task<TimeWindow?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(await base.FirstOrDefaultAsync(id, userId, noTracking));
    }

    private static TimeWindow? Localize(TimeWindow? timeWindow)
    {
        if (timeWindow == default)
        {
            return timeWindow;
        }
        timeWindow.From = timeWindow.From.ToLocalTime();
        timeWindow.Until = timeWindow.Until.ToLocalTime();
        return timeWindow;
    }
    
    private static List<TimeWindow> Localize(List<TimeWindow> timeWindows)
    {
        foreach (var timeWindow in timeWindows)
        {
            timeWindow.From = timeWindow.From.ToLocalTime();
            timeWindow.Until = timeWindow.Until.ToLocalTime();
        }
        return timeWindows;
    }
}