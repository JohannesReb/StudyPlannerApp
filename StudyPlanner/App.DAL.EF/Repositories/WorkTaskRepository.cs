using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Entities;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkTaskRepository : BaseEntityRepository<Domain.DbEntities.WorkTask, App.DAL.DTO.Entities.WorkTask, AppDbContext>, IWorkTaskRepository
{
    public WorkTaskRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.WorkTask, DTO.Entities.WorkTask>(mapper))
    {
    }
    protected override IQueryable<Domain.DbEntities.WorkTask> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var userRoles = RepoDbContext.UserRoles.Where(u => u.UserId.Equals(userId)).Select(u => u.RoleId);
        var workTaskRoles = RepoDbContext.WorkTaskRoles.Where(w => userRoles.Contains(w.RoleId)).Select(w => w.WorkTaskId);
        var workTasks = RepoDbContext.UserWorkTasks.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(w => w.WorkTaskId).ToList();
        
        var query = RepoDbSet.AsQueryable();
        query = query.Include(w => w.Subject).Where(w => workTasks.Contains(w.Id) || workTaskRoles.Contains(w.Id));
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public async Task<List<App.DAL.DTO.Entities.WorkTask>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }
    public async Task<List<App.DAL.DTO.Entities.WorkTask>> GetAllUnPlannedAsync(Guid userId)
    {
        var workTaskTimeWindows = RepoDbContext.WorkTaskTimeWindows.AsQueryable().Select(w => w.WorkTaskId);
        var query = CreateQuery(userId).Where(w => !workTaskTimeWindows.Contains(w.Id)).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        var query = CreateQuery(userId).Where(w => w.SubjectId == subjectId).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }
    public async Task<List<App.DAL.DTO.Entities.WorkTask>> GetAllPublicSortedAsync(Guid userId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var workTasks = RepoDbContext.UserWorkTasks.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(w => w.WorkTaskId).ToList();
        var query = CreateQuery(userId).Where(w => !workTasks.Contains(w.Id) && subjects.Contains(w.SubjectId)).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllPublicSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        var workTasks = RepoDbContext.UserWorkTasks.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(w => w.WorkTaskId).ToList();
        var query = CreateQuery(userId).Where(w => w.SubjectId.Equals(subjectId) && !workTasks.Contains(w.Id)).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }
    public async Task<List<App.DAL.DTO.Entities.WorkTask>> GetAllChosenSortedAsync(Guid userId)
    {
        var subjects = RepoDbContext.UserSubjects.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(u => u.SubjectId).ToList();
        var workTasks = RepoDbContext.UserWorkTasks.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(w => w.WorkTaskId).ToList();
        var query = CreateQuery(userId).Where(w => workTasks.Contains(w.Id) && subjects.Contains(w.SubjectId)).OrderBy(w => w.Deadline);
        var res = await query.ToListAsync();
        
        return Localize(res.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }

    public async Task<List<DTO.Entities.WorkTask>> GetAllChosenSortedOfSubjectAsync(Guid userId, Guid subjectId)
    {
        var workTasks = RepoDbContext.UserWorkTasks.AsQueryable()
            .Where(u => u.UserId.Equals(userId))
            .Select(w => w.WorkTaskId).ToList();
        var query = CreateQuery(userId).Where(w => w.SubjectId.Equals(subjectId) && workTasks.Contains(w.Id)).OrderBy(w => w.Deadline);
        
        return Localize(query.Select(e => Personalize(Mapper.Map(e), userId)).ToList());
    }
    
    public override List<WorkTask> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<WorkTask>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).Select(w => Personalize(w, userId)).ToList());
    }

    public override WorkTask? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Personalize(Localize(base.FirstOrDefault(id, userId, noTracking)), userId);
    }

    public override async Task<WorkTask?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Personalize(Localize(await base.FirstOrDefaultAsync(id, userId, noTracking)), userId);
    }
    
    private static WorkTask? Localize(WorkTask? workTask)
    {
        if (workTask == default)
        {
            return workTask;
        }
        workTask.Deadline = workTask.Deadline?.ToLocalTime();
        return workTask;
    }
    
    private static List<WorkTask> Localize(List<WorkTask> workTasks)
    {
        foreach (var workTask in workTasks)
        {
            workTask.Deadline = workTask.Deadline?.ToLocalTime();
        }
        return workTasks;
    }
    
    private static WorkTask Personalize(WorkTask workTask, Guid userId)
    {
        // var creator = RepoDbContext.UserFields.AsQueryable().First(u => u.Field.Equals(workTask.Field) && u.UserId.Equals(workTask.CreatedBy));
        // var querier = RepoDbContext.UserFields.AsQueryable().First(u => u.Field.Equals(workTask.Field) && u.UserId.Equals(userId));
        // workTask.TimeExpectancy = workTask.TimeExpectancy / creator.Multiplier * querier.Multiplier;
        return workTask;
    }
    
}