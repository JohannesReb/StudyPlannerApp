using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICurriculumRepository : IEntityRepository<App.DAL.DTO.Entities.Curriculum>, ICurriculumRepositoryCustom<App.DAL.DTO.Entities.Curriculum>
{
}

public interface ICurriculumRepositoryCustom<TEntity>
{
    Task<List<TEntity>> GetAllSortedAsync(Guid userId = default);
}