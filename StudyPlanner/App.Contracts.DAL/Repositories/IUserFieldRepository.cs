using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUserFieldRepository : IEntityRepository<App.DAL.DTO.ManyToMany.UserField>
{
    
}