using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IEwentRoleRepository : IEntityRepository<App.DAL.DTO.ManyToMany.EwentRole>
{
    
}