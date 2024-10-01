using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IModuleRepository : IEntityRepository<App.DAL.DTO.Entities.Module>, IModuleRepositoryCustom<App.DAL.DTO.Entities.Module>
{
    // define your custom methods here
}

public interface IModuleRepositoryCustom<TEntity>
{
    Task<List<TEntity>> GetAllSortedAsync(Guid userId);
    Task<List<TEntity>> GetAllSortedOfCurriculumAsync(Guid curriculumId, Guid userId = default);
}