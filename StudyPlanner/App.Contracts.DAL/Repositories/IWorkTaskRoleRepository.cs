using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IWorkTaskRoleRepository : IEntityRepository<App.DAL.DTO.ManyToMany.WorkTaskRole>, IWorkTaskRoleRepositoryCustom<App.DAL.DTO.ManyToMany.WorkTaskRole>
{
    // define your custom methods here
}

public interface IWorkTaskRoleRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
    Task<List<TEntity>> GetAllOfWorkTaskAsync(Guid workTaskId, Guid userId = default, bool noTracking = true);
}
