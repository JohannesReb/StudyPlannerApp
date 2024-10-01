using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ISubjectRepository : IEntityRepository<App.DAL.DTO.Entities.Subject>, ISubjectRepositoryCustom<App.DAL.DTO.Entities.Subject>
{
    // define your custom methods here
}

public interface ISubjectRepositoryCustom<TEntity>
{
    Task<List<TEntity>> GetAllSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllSortedOfModuleAsync(Guid userId, Guid moduleId);
    Task<List<TEntity>> GetAllPublicSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllPublicSortedOfCurriculumAsync(Guid userId, Guid curriculumId);
    Task<List<TEntity>> GetAllChosenSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllChosenSortedOfCurriculumAsync(Guid userId, Guid curriculumId);
    
}