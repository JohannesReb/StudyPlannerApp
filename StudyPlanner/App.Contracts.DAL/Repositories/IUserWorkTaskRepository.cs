using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUserWorkTaskRepository : IEntityRepository<App.DAL.DTO.ManyToMany.UserWorkTask>, IUserWorkTaskRepositoryCustom<App.DAL.DTO.ManyToMany.UserWorkTask>
{
    // define your custom methods here
}

public interface IUserWorkTaskRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
    Task<List<TEntity>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true);
    Task<TEntity?> GetFirstOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true);
}
