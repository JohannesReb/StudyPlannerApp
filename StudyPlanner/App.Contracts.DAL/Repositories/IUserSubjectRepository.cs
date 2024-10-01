using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUserSubjectRepository : IEntityRepository<App.DAL.DTO.ManyToMany.UserSubject>, IUserSubjectRepositoryCustom<App.DAL.DTO.ManyToMany.UserSubject>
{
    // define your custom methods here
}

public interface IUserSubjectRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
    Task<List<TEntity>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true);
    Task<TEntity> GetFirstOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true);
}
