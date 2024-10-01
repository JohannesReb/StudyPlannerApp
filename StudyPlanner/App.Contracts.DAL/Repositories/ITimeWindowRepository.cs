using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ITimeWindowRepository : IEntityRepository<App.DAL.DTO.Entities.TimeWindow>, ITimeWindowRepositoryCustom<App.DAL.DTO.Entities.TimeWindow>
{
    // define your custom methods here
}

public interface ITimeWindowRepositoryCustom<TEntity>
{
    Task<List<TEntity>> GetAllSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllActiveSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllAvailableAsync(Guid userWorkTaskId, Guid userId);
    Task<TEntity?> FindByWorkTaskIdAsync(Guid workTaskId, Guid userId);
    
}