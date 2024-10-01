using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IWorkTaskTimeWindowRepository : IEntityRepository<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow>, IWorkTaskTimeWindowRepositoryCustom<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow>
{
    // define your custom methods here
}

public interface IWorkTaskTimeWindowRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
    Task<TEntity?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true);
    Task<List<TEntity>> GetAllSortedAsync(Guid userId = default, bool noTracking = true);
}
