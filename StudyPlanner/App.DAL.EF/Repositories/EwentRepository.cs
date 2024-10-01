using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Entities;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EwentRepository : BaseEntityRepository<Domain.DbEntities.Ewent, App.DAL.DTO.Entities.Ewent, AppDbContext>, IEwentRepository
{
    public EwentRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<Domain.DbEntities.Ewent, DTO.Entities.Ewent>(mapper))
    {
    }
    protected override IQueryable<Domain.DbEntities.Ewent> CreateQuery(Guid userId = default, bool noTracking = true)
    {
        var userEwents = RepoDbContext.UserEwents.Where(u => u.UserId.Equals(userId)).Select(u => u.EwentId);
        var query = RepoDbSet.AsQueryable().Where(e => userEwents.Contains(e.Id));
        query = query.Include(e => e.Subject);
        foreach (var ewent in query)
        {
            ewent.From = ewent.From.ToLocalTime();
            ewent.Until = ewent.Until.ToLocalTime();
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public override List<Ewent> GetAll(Guid userId = default, bool noTracking = true)
    {
        return Localize(base.GetAll(userId, noTracking).ToList());
    }

    public override async Task<List<Ewent>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return Localize((await base.GetAllAsync(userId, noTracking)).ToList());
    }

    public override Ewent? FirstOrDefault(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(base.FirstOrDefault(id, userId, noTracking));
    }

    public override async Task<Ewent?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Localize(await base.FirstOrDefaultAsync(id, userId, noTracking));
    }

    private static Ewent? Localize(Ewent? ewent)
    {
        if (ewent == default)
        {
            return ewent;
        }
        ewent.From = ewent.From.ToLocalTime();
        ewent.Until = ewent.Until.ToLocalTime();
        return ewent;
    }
    
    private static List<Ewent> Localize(List<Ewent> ewents)
    {
        foreach (var ewent in ewents)
        {
            ewent.From = ewent.From.ToLocalTime();
            ewent.Until = ewent.Until.ToLocalTime();
        }
        return ewents;
    }
}