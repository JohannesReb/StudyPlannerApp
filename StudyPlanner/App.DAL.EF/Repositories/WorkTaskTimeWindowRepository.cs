using App.Contracts.DAL.Repositories;
using App.DAL.DTO.ManyToMany;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkTaskTimeWindowRepository : BaseEntityRepository<Domain.ManyToMany.WorkTaskTimeWindow, App.DAL.DTO.ManyToMany.WorkTaskTimeWindow, AppDbContext>, IWorkTaskTimeWindowRepository
{
    public WorkTaskTimeWindowRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.ManyToMany.WorkTaskTimeWindow, DTO.ManyToMany.WorkTaskTimeWindow>(mapper))
    {
    }
    protected override IQueryable<Domain.ManyToMany.WorkTaskTimeWindow> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        query = query
            .Include(w => w.TimeWindow)
            .Include(w => w.WorkTask)
            .Where(w => w.TimeWindow!.UserId.Equals(userId));

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    
    public async Task<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true)
    {
        return Localize(await CreateQuery(userId, noTracking)
            .Where(u => u.WorkTaskId.Equals(workTaskId))
            .Select(de => Mapper.Map(de)).FirstOrDefaultAsync());
    }
    public async Task<List<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow>> GetAllSortedAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(userId).OrderBy(w => w.TimeWindow!.From);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Mapper.Map(e)).ToList());
    }
    
    public override List<WorkTaskTimeWindow> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<WorkTaskTimeWindow>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).ToList());
    }

    public override WorkTaskTimeWindow? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(base.FirstOrDefault(id, userId, noTracking));
    }

    public override async Task<WorkTaskTimeWindow?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(await base.FirstOrDefaultAsync(id, userId, noTracking));
    }
    
    private static WorkTaskTimeWindow? Localize(WorkTaskTimeWindow? workTaskTimeWindow)
    {
        if (workTaskTimeWindow == default)
        {
            return workTaskTimeWindow;
        }
        workTaskTimeWindow.WorkTask!.Deadline = workTaskTimeWindow.WorkTask.Deadline?.ToLocalTime();
        workTaskTimeWindow.TimeWindow!.From =  workTaskTimeWindow.TimeWindow!.From.ToLocalTime();
        workTaskTimeWindow.TimeWindow!.Until =  workTaskTimeWindow.TimeWindow!.Until.ToLocalTime();
        return workTaskTimeWindow;
    }
    
    private static List<WorkTaskTimeWindow> Localize(List<WorkTaskTimeWindow> workTaskTimeWindows)
    {
        foreach (var workTaskTimeWindow in workTaskTimeWindows)
        {
            workTaskTimeWindow.WorkTask!.Deadline = workTaskTimeWindow.WorkTask.Deadline?.ToLocalTime();
            workTaskTimeWindow.TimeWindow!.From =  workTaskTimeWindow.TimeWindow!.From.ToLocalTime();
            workTaskTimeWindow.TimeWindow!.Until =  workTaskTimeWindow.TimeWindow!.Until.ToLocalTime();
        }
        return workTaskTimeWindows;
    }
}