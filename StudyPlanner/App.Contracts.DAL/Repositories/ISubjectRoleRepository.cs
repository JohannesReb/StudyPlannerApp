using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ISubjectRoleRepository : IEntityRepository<App.DAL.DTO.ManyToMany.SubjectRole>, ISubjectRoleRepositoryCustom<App.DAL.DTO.ManyToMany.SubjectRole>
{
    // define your custom methods here
}

public interface ISubjectRoleRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
    Task<List<TEntity>> GetAllOfSubjectAsync(Guid subjectId, Guid userId = default, bool noTracking = true);
}
