using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;
public interface IWorkTaskRepository : IEntityRepository<App.DAL.DTO.Entities.WorkTask>, IWorkTaskRepositoryCustom<App.DAL.DTO.Entities.WorkTask>
{
    // define your custom methods here
}

public interface IWorkTaskRepositoryCustom<TEntity>
{
    Task<List<TEntity>> GetAllSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllUnPlannedAsync(Guid userId);
    Task<List<TEntity>> GetAllSortedOfSubjectAsync(Guid userId, Guid subjectId);
    Task<List<TEntity>> GetAllPublicSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllPublicSortedOfSubjectAsync(Guid userId, Guid subjectId);
    Task<List<TEntity>> GetAllChosenSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllChosenSortedOfSubjectAsync(Guid userId, Guid subjectId);
    // Task<IEnumerable<TEntity>> GetAllCodesAsync(Guid userId);
}